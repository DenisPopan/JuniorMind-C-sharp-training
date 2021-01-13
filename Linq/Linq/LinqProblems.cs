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

            return text
                .GroupBy(character => character)
                .First(group => group.Count() == 1)
                .Key;
        }

        public static int ToInt(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            var textToIntArray = text
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

            return text
                .GroupBy(x => x)
                .Aggregate((x, y) => x.Count() > y.Count() ? x : y)
                .Key;
        }

        public static IEnumerable<string> PalindromicPartitions(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return Enumerable.Range(0, text.Length)
                .SelectMany(x =>
                    Enumerable.Range(1, text.Length - x)
                        .Select(y => (x, y)))
                .Select(x => text.Substring(x.x, x.y))
                .Where(x => x.SequenceEqual(x.Reverse()));
        }

        public static IEnumerable<IEnumerable<int>> SubarraysSumEquals(this IEnumerable<int> array, int k)
        {
            EnsureIsNotNull(array, nameof(array));

            return Enumerable.Range(0, array.Count())
                .SelectMany(x =>
                    Enumerable.Range(1, array.Count() - x)
                        .Select(y => (x, y)))
                .Select(x => array.Skip(x.x).Take(x.y))
                .Where(x => x.Sum() <= k);
        }

        public static IEnumerable<IEnumerable<int>> SumCombinations(int n, int k)
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
                .Select(x => x.ToArray());
        }

        public static IEnumerable<(double, double, double)> PythagoreanNumbers(this IEnumerable<int> array)
        {
            var squareNumbers = array.OrderBy(x => x).Select(x => x * x);

            return squareNumbers
                .SelectMany(squareNumber => squareNumbers.SkipWhile(x => x <= squareNumber)
                    .Join(
                        squareNumbers.SkipWhile(y => y <= squareNumber).Skip(1),
                        a => a + squareNumber,
                        b => b,
                        (a, b) => (Math.Sqrt(squareNumber), Math.Sqrt(a), Math.Sqrt(b))));
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
                    .Aggregate((x, y) => x.Score > y.Score ? x : y));
        }

        public static IEnumerable<string> TopWords(this string text, int topNumber = 3)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.Split(" .,?!".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(word => word.ToLower())
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(topNumber);
        }

        public static bool IsSudokuValid(this int[,] sudoku)
        {
            EnsureIsNotNull(sudoku, nameof(sudoku));

            if (sudoku.GetLength(0) != sudoku.GetLength(1))
            {
                return false;
            }

            var submatricesStartIndex = Enumerable.Range(0, (int)Math.Sqrt(sudoku.GetLength(0)))
                .Select(x => x * (int)Math.Sqrt(sudoku.GetLength(0)));

            var submatricesEndIndex = submatricesStartIndex.Select(x => x + (int)Math.Sqrt(sudoku.GetLength(0)) - 1);

            return Enumerable.Range(0, sudoku.GetLength(0))
                .All(x => sudoku.Row(x).FollowsSudokuRules(sudoku.GetLength(0))
                        && sudoku.Column(x).FollowsSudokuRules(sudoku.GetLength(0)))
                && sudoku.Submatrices(submatricesStartIndex, submatricesEndIndex)
                            .All(x => x.FollowsSudokuRules(sudoku.GetLength(0)));
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

        static bool FollowsSudokuRules(this IEnumerable<int> array, int sudokuSize)
        {
            return array.All(x => x > 0 && x <= sudokuSize) && array.Distinct().Count() == sudokuSize;
        }

        static IEnumerable<int> Row(this int[,] sudoku, int row)
        {
            return Enumerable.Range(0, sudoku.GetLength(0)).Select(x => sudoku[row, x]);
        }

        static IEnumerable<int> Column(this int[,] sudoku, int column)
        {
            return Enumerable.Range(0, sudoku.GetLength(0)).Select(x => sudoku[x, column]);
        }

        static IEnumerable<IEnumerable<int>> Submatrices(this int[,] sudoku, IEnumerable<int> submatricesStartIndex, IEnumerable<int> submatricesEndIndex)
        {
            return submatricesStartIndex.SelectMany(x => submatricesStartIndex
                    .Select(y => (x, y)))
                .Zip(
                    submatricesEndIndex.SelectMany(x => submatricesEndIndex.Select(y => (x, y))),
                    (a, b) => SubMatrix(sudoku, a, b));
        }

        static IEnumerable<int> SubMatrix(this int[,] sudoku, (int, int) start, (int, int) stop)
        {
            return Enumerable.Range(start.Item1, stop.Item1 - start.Item1 + 1)
                .SelectMany(x => Enumerable.Range(start.Item2, stop.Item2 - start.Item2 + 1)
                    .Select(y => sudoku[x, y]));
        }
    }
}
