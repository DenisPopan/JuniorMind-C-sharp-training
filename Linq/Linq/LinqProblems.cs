﻿using System;
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
