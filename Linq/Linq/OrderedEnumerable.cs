using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        readonly IEnumerable<TElement> source;

        public OrderedEnumerable(IEnumerable<TElement> source)
        {
            this.source = source;
        }

        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(
            Func<TElement, TKey> keySelector,
            IComparer<TKey> comparer,
            bool descending)
        {
            var list = source.ToList();
            bool isSorted = true;
            var condition = 1;
            if (descending)
            {
                condition = -1;
            }

            do
            {
                isSorted = true;

                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (comparer.Compare(keySelector(list[i]), keySelector(list[i + 1])) * condition > 0)
                    {
                        var aux = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = aux;
                        isSorted = false;
                    }
                }
            }
            while (!isSorted);

            return new OrderedEnumerable<TElement>(list);
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            foreach (var element in source)
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
