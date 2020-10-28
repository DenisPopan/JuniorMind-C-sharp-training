using System.Collections.Generic;

namespace IntegersArray
{
    public class ReadOnlyListDecorator<T> : ListDecorator<T>
    {
        public ReadOnlyListDecorator(IList<T> list)
            : base(list)
        {
        }

        public override bool IsReadOnly
        {
            get
            {
                return true;
            }
        }
    }
}
