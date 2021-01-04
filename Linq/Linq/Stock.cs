using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StockFacts.cs")]

namespace Linq
{
    public class Stock
    {
        readonly List<Product> list;
        readonly List<StockUser> stockUsers;

        public Stock()
        {
            list = new List<Product>();
            stockUsers = new List<StockUser>();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void AddUser(StockUser user)
        {
            LinqProblems.EnsureIsNotNull(user, nameof(user));
            stockUsers.Add(user);
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

        public string ProductQuantity(string name)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));

            var productIndex = FindProductIndex(name);
            ProductExists(productIndex);

            return $"There are {list[productIndex].Quantity} items of type {name} in stock";
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
                    return $"Product stock has less than 2 items!({quantity})";

                case int n when n < 5:
                    return $"Product stock has less than 5 items!({quantity})";

                case int n when n < 10:
                    return $"Product stock has less than 10 items!({quantity})";
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
