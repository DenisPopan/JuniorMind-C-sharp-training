using System;
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

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void AddProduct(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));

            if (list.FindIndex(product => string.Equals(product.Name, name, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                throw new ArgumentException("Product already exists!");
            }

            list.Add(new Product { Name = name, Quantity = quantity });
        }

        public string Status()
        {
            var status = list.Select(product => $"Product: {product.Name}, Quantity: {product.Quantity}\n");
            var statusString = "";
            foreach (var element in status)
            {
                statusString += element;
            }

            return statusString;
        }
    }
}
