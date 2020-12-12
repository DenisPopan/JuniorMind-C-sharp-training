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

            return text.ToCharArray().Count(x => "aeiouAEIOU".Contains(x));
        }

        public static int ConsonantsNumber(this string text)
        {
            EnsureIsNotNull(text, nameof(text));

            return text.Length - VowelsNumber(text);
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

        static bool FollowsSudokuRules(this IEnumerable<int> array)
        {
            return array.All(x => x > 0 && x <= 9) && array.Distinct().Count() == 9;
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

        static void EnsureIsNotNull<T>(T source, string name)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }
    }
}
