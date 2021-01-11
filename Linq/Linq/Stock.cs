using System;
using System.Collections.Generic;

namespace Linq
{
    public class Stock
    {
        readonly List<Product> list;
        readonly Action<Product> callback;

        public Stock(Action<Product> callback)
        {
            list = new List<Product>();
            this.callback = callback;
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

            if (FindProductIndex(name) >= 0)
            {
                throw new ArgumentException("Product already exists!");
            }

            list.Add(new Product { Name = name, Quantity = quantity });
        }

        public string Status()
        {
            return list.Select(product => $"Product: {product.Name}, Quantity: {product.Quantity}\n")
                .Aggregate("", (x, y) => x + y);
        }

        public int ProductQuantity(string name)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));

            ProductExists(name);
            var productIndex = FindProductIndex(name);

            return list[productIndex].Quantity;
        }

        public void SellProductStock(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));

            var productIndex = FindProductIndex(name);
            ProductExists(name);

            var productQuantity = list[productIndex].Quantity;

            if (productQuantity < quantity)
            {
                throw new ArgumentException($"You can only sell {productQuantity} items!");
            }

            list[productIndex].Quantity -= quantity;

            if (list[productIndex].Quantity >= 10)
            {
                return;
            }

            SendNotification(list[productIndex]);
        }

        void SendNotification(Product product)
        {
            LinqProblems.EnsureIsNotNull(product, nameof(product));
            LinqProblems.EnsureIsNotNull(callback, nameof(callback));

            callback(product);
        }

        int FindProductIndex(string name)
        {
            return list.FindIndex(product => string.Equals(product.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        void ProductExists(string name)
        {
            if (list.Any(product => product.Name.Equals(name)))
            {
                return;
            }

            throw new ArgumentException("Product not found.");
        }
    }
}
