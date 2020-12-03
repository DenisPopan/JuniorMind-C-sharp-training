using System.Collections.Generic;

namespace Linq
{
    class CombinedComparers<TKey> : IComparer<TKey>
    {
        readonly IComparer<TKey> orderByComparer;
        readonly IComparer<TKey> thenByComparer;

        public CombinedComparers(IComparer<TKey> orderByComparer, IComparer<TKey> thenByComparer)
        {
            this.orderByComparer = orderByComparer;
            this.thenByComparer = thenByComparer;
        }

        public int Compare(TKey x, TKey y)
        {
            var orderByComparerResult = orderByComparer.Compare(x, y);
            if (orderByComparerResult != 0)
            {
                return orderByComparerResult;
            }

            return thenByComparer.Compare(x, y);
        }
    }
}
