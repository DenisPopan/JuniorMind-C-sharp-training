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

            if (FindProductIndex(name) >= 0)
            {
                throw new ArgumentException("Product already exists!");
            }

            list.Add(new Product { Name = name, Quantity = quantity });
        }

        public void RemoveProduct(string name)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));

            var productIndex = FindProductIndex(name);

            ProductExists(productIndex);

            list.Remove(list[productIndex]);
        }

        public string Status()
        {
            Func<int, string> callback = StockWarning;
            var status = list.Select(product => $"Product: {product.Name}, Quantity: {callback(product.Quantity)}\n");
            var statusString = "";
            foreach (var element in status)
            {
                statusString += element;
            }

            return statusString;
        }

        public string ProductQuantity(string name)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));

            var productIndex = FindProductIndex(name);
            ProductExists(productIndex);

            Func<int, string> callback = StockWarning;

            return callback(list[productIndex].Quantity);
        }

        public void BuyNewStock(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));

            var productIndex = FindProductIndex(name);
            ProductExists(productIndex);

            list[productIndex].Quantity += quantity;
        }

        public string SellProductStock(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));

            var productIndex = FindProductIndex(name);
            ProductExists(productIndex);

            list[productIndex].Quantity -= quantity;

            Func<int, string> callback = StockWarning;

            return callback(list[productIndex].Quantity);
        }

        static string StockWarning(int quantity)
        {
            switch (quantity)
            {
                case int n when n < 2:
                    return "Product stock has less than 2 items!";

                case int n when n < 5:
                    return "Product stock has less than 5 items!";

                case int n when n < 10:
                    return "Product stock has less than 10 items!";
                default:
                    return quantity.ToString();
            }
        }

        int FindProductIndex(string name)
        {
            return list.FindIndex(product => string.Equals(product.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        void ProductExists(int productIndex)
        {
            if (productIndex >= 0)
            {
                return;
            }

            throw new ArgumentException("Product not found.");
        }
    }
}
