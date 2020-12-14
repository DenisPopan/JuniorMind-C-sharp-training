using System.Collections.Generic;

namespace Linq
{
    class Stock<Product>
    {
        readonly List<Product> list;

        public Stock()
        {
            list = new List<Product>();
        }
    }
}
