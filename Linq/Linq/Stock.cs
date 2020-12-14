using System.Collections.Generic;

namespace Linq
{
    public class Stock<TProduct>
    {
        readonly List<Product> list;

        public Stock()
        {
            list = new List<Product>();
        }

        public int Count { get; internal set; }

        public void AddProduct(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));
            list.Add(new Product { Name = name, Quantity = quantity });
            Count++;
        }
    }
}
