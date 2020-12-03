using System;
using System.Collections.Generic;

namespace Linq
{
    internal class MainComparer<TKey, TElement> : IComparer<TElement>
    {
        readonly Func<TElement, TKey> keySelector;
        readonly IComparer<TKey> comparer;

        internal MainComparer(Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public int Compare(TElement x, TElement y)
        {
            return comparer.Compare(keySelector(x), keySelector(y));
        }
    }
}
