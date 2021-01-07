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
        public static (int, int) VowelsAndConsonantsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            var vowelsNumber = text.Count(x => "aeiouAEIOU".Contains(x));

            return (vowelsNumber, text.Length - vowelsNumber);
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

            var sign = 1;

            if (textToIntArray.First() == -1)
            {
                sign = -1;
                textToIntArray = textToIntArray.Skip(1);
            }

            return textToIntArray.Aggregate(0, (x, y) => x * 10 + y) * sign;
        }

        public static char MaxOccurrence(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.AsEnumerable()
                .GroupBy(x => x)
                .Aggregate((x, y) => x.Count() > y.Count() ? x : y)
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
            var range = Enumerable.Range(1, n);

            var binaryNumbers = Enumerable.Range(0, (int)Math.Pow(2, n))
                .Select(x => Convert.ToString(x, 2).ToArray())
                .Select(x => Enumerable.Repeat('0', n - x.Length).Concat(x));

            return binaryNumbers
                .Select(binaryNumber => range
                    .Zip(binaryNumber, (rangeElement, digit) =>
                        digit == '0' ? rangeElement : rangeElement * -1))
                .Where(combination => combination.Sum() == k)
                .Select(array => array
                    .Select(x => x < 0 ? $"{x}" : "+" + x)
                    .Aggregate("", (x, y) => x + y) + $"={k}");
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

            return Enumerable.Range(0, sudoku.GetLength(0))
                .All(x => sudoku.Row(x).FollowsSudokuRules()
                        && sudoku.Column(x).FollowsSudokuRules());
        }

        public static double PolishArithmeticResult(this string operation)
        {
            EnsureIsNotNull(operation, nameof(operation));

            return operation.Split(' ').Aggregate(Enumerable.Empty<double>(), (y, z) =>
                double.TryParse(z, out double element) ?
                y.Append(element) :
                y.SkipLast(2)
                    .Append(OperationResult(z, y.TakeLast(2).First(), y.TakeLast(2).Last())))
                .Last();
        }

        internal static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
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
                    throw new ArgumentException("The given string is not an operator!", stringOperator);
            }
        }

        static bool FollowsSudokuRules(this IEnumerable<int> array)
        {
            return array.All(x => x > 0 && x <= array.Count()) && array.Distinct().Count() == array.Count();
        }

        static IEnumerable<int> Row(this int[,] array, int row)
        {
            return Enumerable.Range(0, array.GetLength(0)).Select(x => array[row, x]);
        }

        static IEnumerable<int> Column(this int[,] array, int column)
        {
            return Enumerable.Range(0, array.GetLength(0)).Select(x => array[x, column]);
        }
    }
}
