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

            var query = from character in text.ToCharArray()
                        group character by character;

            foreach (var group in query)
            {
                if (group.Count() == 1)
                {
                    return group.Key;
                }
            }

            return '-';
        }

        public static int ToInt(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            int number = 0;
            int sign = 1;

            if (text[0] == '-')
            {
                sign = -1;
            }

            foreach (var digit in text.ToCharArray().Select(x => char.GetNumericValue(x)).Where(x => x > -1))
            {
                number = number * 10 + (int)digit;
            }

            return number * sign;
        }

        public static char MaxOccurrence(this string text)
        {
            EnsureIsNotNull(text, nameof(text));
            int maxOccurrence = 0;
            char maxOccurrenceChar = '-';
            foreach (var group in text.ToCharArray().GroupBy(x => x))
            {
                if (group.Count() > maxOccurrence)
                {
                    maxOccurrence = group.Count();
                    maxOccurrenceChar = group.Key;
                }
            }

            return maxOccurrenceChar;
        }

        public static IEnumerable<string> PalindromicPartitions(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            var charArray = text.ToCharArray();
            int index = 1;
            int length = charArray.Length;
            while (length > 0 && index <= length)
            {
                var partition = charArray.TakeLast(length).Take(index);
                if (partition.SequenceEqual(partition.Reverse()))
                {
                    yield return new string(partition.ToArray());
                }

                if (index == length)
                {
                    length--;
                    index = 0;
                }

                index++;
            }
        }

        public static IEnumerable<IEnumerable<int>> SubarraysSum(this IEnumerable<int> array, int k)
        {
            EnsureIsNotNull(array, nameof(array));

            int index = 1;
            int length = array.Count();
            while (length > 0 && index <= length)
            {
                var subarray = array.TakeLast(length).Take(index);
                if (subarray.Sum() <= k)
                {
                    yield return subarray;
                }

                if (index == length)
                {
                    length--;
                    index = 0;
                }

                index++;
            }
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
            foreach (var squareNumber in squareNumbers)
            {
                var allOtherSquares = squareNumbers.SkipWhile(x => x <= squareNumber);
                var allOtherSquaresButOne = allOtherSquares.Skip(1);

                foreach (var combination in allOtherSquares
                    .Join(
                        allOtherSquaresButOne,
                        x => x + squareNumber,
                        y => y,
                        (x, y) => $"[{Math.Sqrt(squareNumber)}, {Math.Sqrt(x)}, {Math.Sqrt(y)}]"))
                {
                    yield return combination;
                }
            }
        }

        public static IEnumerable<Product> AtLeastOneFeature(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList.Where(product => product.Features.Intersect(featureList).Any());
        }

        public static IEnumerable<Product> AllFeatures(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList.Where(product => product.Features.Intersect(featureList).Count() == featureList.Count());
        }

        public static IEnumerable<Product> NotTheseFeatures(this IEnumerable<Product> productList, IEnumerable<Feature> featureList)
        {
            return productList.Except(productList.AtLeastOneFeature(featureList));
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

            var topWords = text.Split(" .,?!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word.ToLower())
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key);

            int index = 0;
            foreach (var word in topWords)
            {
                index++;
                yield return word;
                if (index == 3)
                {
                    break;
                }
            }
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
