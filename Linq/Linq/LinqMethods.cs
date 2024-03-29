using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    public static class LinqMethods
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(predicate, nameof(source));

            foreach (var element in source)
            {
                if (!predicate(element))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(predicate, nameof(source));

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(predicate, nameof(source));

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            throw new InvalidOperationException();
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(selector, nameof(selector));

            foreach (var element in source)
            {
                yield return selector(element);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(selector, nameof(selector));

            foreach (var element in source)
            {
                foreach (var returnedElement in selector(element))
                {
                    yield return returnedElement;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(predicate, nameof(predicate));

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    yield return element;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(keySelector, nameof(keySelector));
            EnsureIsNotNull(elementSelector, nameof(elementSelector));

            var dictionary = new Dictionary<TKey, TElement>();

            foreach (var element in source)
            {
                dictionary.Add(keySelector(element), elementSelector(element));
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            EnsureIsNotNull(first, nameof(first));
            EnsureIsNotNull(second, nameof(second));
            EnsureIsNotNull(resultSelector, nameof(resultSelector));

            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(func, nameof(func));
            EnsureIsNotNull(seed, nameof(seed));

            var acummulator = seed;

            foreach (var element in source)
            {
                acummulator = func(acummulator, element);
            }

            return acummulator;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            EnsureIsNotNull(outer, nameof(outer));
            EnsureIsNotNull(inner, nameof(inner));
            EnsureIsNotNull(outerKeySelector, nameof(outerKeySelector));
            EnsureIsNotNull(innerKeySelector, nameof(innerKeySelector));
            EnsureIsNotNull(resultSelector, nameof(resultSelector));

            foreach (var outerElement in outer)
            {
                foreach (var innerElement in inner)
                {
                    if (outerKeySelector(outerElement).Equals(innerKeySelector(innerElement)))
                    {
                        yield return resultSelector(outerElement, innerElement);
                    }
                }
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(
            this IEnumerable<TSource> source,
            IEqualityComparer<TSource> comparer)
        {
            EnsureIsNotNull(source, nameof(source));

            return new HashSet<TSource>(source, comparer);
        }

        public static IEnumerable<TSource> Union<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            EnsureIsNotNull(first, nameof(first));
            EnsureIsNotNull(second, nameof(second));

            var union = new HashSet<TSource>(first, comparer);
            union.UnionWith(new HashSet<TSource>(second, comparer));
            return union;
        }

        public static IEnumerable<TSource> Intersect<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            EnsureIsNotNull(first, nameof(first));
            EnsureIsNotNull(second, nameof(second));

            var intersection = new HashSet<TSource>(first, comparer);
            intersection.IntersectWith(new HashSet<TSource>(second, comparer));
            return intersection;
        }

        public static IEnumerable<TSource> Except<TSource>(
            this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            IEqualityComparer<TSource> comparer)
        {
            EnsureIsNotNull(first, nameof(first));
            EnsureIsNotNull(second, nameof(second));

            var exception = new HashSet<TSource>(first, comparer);
            exception.ExceptWith(new HashSet<TSource>(second, comparer));
            return exception;
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector,
            Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(elementSelector, nameof(elementSelector));
            EnsureIsNotNull(keySelector, nameof(keySelector));
            EnsureIsNotNull(resultSelector, nameof(resultSelector));
            EnsureIsNotNull(comparer, nameof(comparer));

            var dictionary = new Dictionary<TKey, List<TElement>>();

            foreach (var element in source)
            {
                var key = keySelector(element);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, new List<TElement>());
                }

                dictionary[key].Add(elementSelector(element));
            }

            foreach (var group in dictionary)
            {
                yield return resultSelector(group.Key, group.Value);
            }
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(keySelector, nameof(keySelector));

            return new OrderedEnumerable<TSource>(source, new MainComparer<TKey, TSource>(keySelector, comparer));
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            EnsureIsNotNull(source, nameof(source));
            EnsureIsNotNull(keySelector, nameof(keySelector));
            return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
        }

        // Helper methods
        public static IEnumerable<string> SelectManySelector<T>(T element)
        {
            for (int i = 0; i < 2; i++)
            {
                yield return element.ToString();
            }
        }

        public static string Count<T>(int key, IEnumerable<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            int count = 0;

            foreach (var element in elements)
            {
                count++;
            }

            return $"{key}:{count}";
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
