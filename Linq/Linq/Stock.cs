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
            ProductExists(name);

            return $"There are {list[productIndex].Quantity} items of type {name} in stock";
        }

        public string SellProductStock(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));

            var productIndex = FindProductIndex(name);
            ProductExists(name);

            list[productIndex].Quantity -= quantity;

            Func<string, int, string> callback = StockWarning;

            return callback(name, list[productIndex].Quantity);
        }

        string StockWarning(string name, int quantity)
        {
            switch (quantity)
            {
                case int n when n < 2:
                    return $"Hurry! There are only {quantity} {name}s left!";

                case int n when n < 5:
                    return $"Tik-Tok! Only {quantity} {name}s left!";

                case int n when n < 10:
                    return $"Stock reaches its end! There are {quantity} {name}s left!";
                default:
                    return "Success!";
            }
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
