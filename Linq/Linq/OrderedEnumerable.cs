using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        readonly IEnumerable<TElement> source;
        readonly IComparer<TElement> comparer;

        public OrderedEnumerable(IEnumerable<TElement> source, IComparer<TElement> comparer)
        {
            this.source = source;
            this.comparer = comparer;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            var list = source.ToList();
            bool isSorted = true;
            do
            {
                isSorted = true;

                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (comparer.Compare(list[i], list[i + 1]) > 0)
                    {
                        var aux = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = aux;
                        isSorted = false;
                    }
                }
            }
            while (!isSorted);

            foreach (var element in list)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
