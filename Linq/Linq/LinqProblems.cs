using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    public struct ProductStruct
    {
        public string Name;
        public int Quantity;
    }

    public static class LinqProblems
    {
        public static int VowelsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.AsEnumerable()
                .Count(x => "aeiouAEIOU"
                    .Contains(x));
        }

        public static int ConsonantsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.AsEnumerable()
                .Count(x => !"aeiouAEIOU"
                    .Contains(x));
        }

        public static char FirstUnique(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.AsEnumerable()
                .GroupBy(character => character)
                .First(group => group.Count() == 1)
                .Key;
        }

        public static int ToInt(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            var textToIntArray = text.AsEnumerable()
                .Select(x => Convert.ToInt32(char.GetNumericValue(x)));

            return textToIntArray
                .Aggregate(0, (x, y) => x * 10 + y);
        }

        public static char MaxOccurrence(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.AsEnumerable()
                .GroupBy(x => x)
                .OrderByDescending(group => group.Count())
                .First()
                .Key;
        }

        public static IEnumerable<string> PalindromicPartitions(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            var allSubarrays = Enumerable.Range(1, text.Length)
                .Select(x => text
                    .AsEnumerable()
                    .TakeLast(x));

            var allPartitions = allSubarrays
                .Select(x => Enumerable
                    .Range(1, x.Count())
                    .Select(y => x.Take(y)));

            var palindromicPartitions = allPartitions
                .SelectMany(x => x
                    .Where(y => y
                        .SequenceEqual(y.Reverse())));

            return palindromicPartitions
                .Select(x => new string(x.ToArray()));
        }

        public static IEnumerable<IEnumerable<int>> SubarraysSumEquals(this IEnumerable<int> array, int k)
        {
            EnsureIsNotNull(array, nameof(array));

            var allSubarrays = Enumerable.Range(1, array.Count())
                .Select(x => array
                    .TakeLast(x));

            var allPartitions = allSubarrays
                .Select(x => Enumerable
                    .Range(1, x.Count())
                    .Select(y => x.Take(y)));

            return allPartitions
                .SelectMany(x => x
                    .Where(y => y
                        .Sum() <= k));
        }

        public static IEnumerable<string> SumCombinations(int n, int k)
        {
            var combinationsNumber = (int)Math.Pow(2, n) - 1;
            var range = Enumerable.Range(1, n);

            while (combinationsNumber >= 0)
            {
                var binaryNumber = Convert.ToString(combinationsNumber, 2);

                while (binaryNumber.Length < n)
                {
                    binaryNumber = "0" + binaryNumber;
                }

                var result = range.Zip(binaryNumber.ToArray(), (rangeElement, digit) => digit switch
                {
                    '0' => rangeElement,
                    '1' => rangeElement * -1,
                    _ => rangeElement
                });

                if (result.Sum() == k)
                {
                    yield return result.ConvertToString() + $"={k}";
                }

                combinationsNumber--;
            }
        }

        public static IEnumerable<string> PythagoreanNumbers(this IEnumerable<int> array)
        {
            var squareNumbers = array.OrderBy(x => x).Select(x => x * x);
            return squareNumbers
                .SelectMany(squareNumber => squareNumbers.SkipWhile(x => x <= squareNumber)
                    .Join(
                        squareNumbers.SkipWhile(y => y <= squareNumber).Skip(1),
                        a => a + squareNumber,
                        b => b,
                        (a, b) => $"[{Math.Sqrt(squareNumber)}, {Math.Sqrt(a)}, {Math.Sqrt(b)}]"));
        }

        public static IEnumerable<Product> AtLeastOneFeature(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList
                .Where(product => product.Features
                    .Intersect(featureList)
                        .Any());
        }

        public static IEnumerable<Product> AllFeatures(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList
                .Where(product => product.Features
                    .Intersect(featureList).Count() == featureList.Count());
        }

        public static IEnumerable<Product> NotTheseFeatures(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList
                .Except(productList
                    .AtLeastOneFeature(featureList));
        }

        public static IEnumerable<ProductStruct> TotalQuantity(this ProductStruct[] firstList, ProductStruct[] secondList)
        {
            return firstList.Union(secondList)
                .GroupBy(x => x.Name)
                .Select(productGroup => new ProductStruct
                {
                    Name = productGroup.Key,
                    Quantity = productGroup.Sum(product => product.Quantity)
                });
        }

        public static IEnumerable<TestResults> HighestTestScore(this TestResults[] list)
        {
            return list.GroupBy(x => x.FamilyId)
                .Select(group => group
                    .OrderByDescending(testResult => testResult.Score)
                        .First());
        }

        public static IEnumerable<string> TopWords(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.Split(" .,?!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word.ToLower())
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(3);
        }

        public static bool IsSudokuValid(this int[,] sudoku)
        {
            EnsureIsNotNull(sudoku, nameof(sudoku));
            var rowsNumber = sudoku.GetLength(0);
            var columnsNumber = sudoku.GetLength(1);

            if (rowsNumber != columnsNumber)
            {
                return false;
            }

            for (int i = 0; i < rowsNumber; i++)
            {
                if (!(sudoku.Row(i).FollowsSudokuRules() && sudoku.Column(i).FollowsSudokuRules()))
                {
                    return false;
                }
            }

            return true;
        }

        public static double PolishArithmeticResult(this string operation)
        {
            EnsureIsNotNull(operation, nameof(operation));

            var splitOperation = operation.Split(' ');
            var list = new List<double>();
            foreach (string element in splitOperation)
            {
                if (double.TryParse(element, out double convertedElement))
                {
                    list.Add(convertedElement);
                }
                else
                {
                    var lastElement = list.Last();
                    list.Remove(lastElement);
                    list[^1] = OperationResult(element, list.Last(), lastElement);
                }
            }

            return list.Last();
        }

        internal static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }

        static string ConvertToString(this IEnumerable<int> array)
        {
            EnsureIsNotNull(array, nameof(array));
            var resultString = "";
            foreach (var element in array)
            {
                if (element > 0)
                {
                    resultString += "+" + element;
                }
                else
                {
                    resultString += element;
                }
            }

            return resultString;
        }

        static double OperationResult(string stringOperator, double lastButOne, double lastElement)
        {
            switch (stringOperator)
            {
                case "+":
                    return lastButOne + lastElement;
                case "-":
                    return lastButOne - lastElement;
                case "*":
                    return lastButOne * lastElement;
                case "/":
                    return lastButOne / lastElement;
                default:
                    throw new ArgumentException("The given string is not an operator!");
            }
        }

        static bool FollowsSudokuRules(this IEnumerable<int> array)
        {
            return array.All(x => x > 0 && x <= array.Count()) && array.Distinct().Count() == array.Count();
        }

        static IEnumerable<int> Row(this int[,] array, int row)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                yield return array[row, i];
            }
        }

        static IEnumerable<int> Column(this int[,] array, int column)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                yield return array[i, column];
            }
        }
    }
}
