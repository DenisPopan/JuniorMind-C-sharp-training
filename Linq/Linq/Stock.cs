using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StockFacts.cs")]

namespace Linq
{
    public class Stock
    {
        readonly List<Product> list;
        readonly List<AbstractUser> users;

        public Stock()
        {
            list = new List<Product>();
            users = new List<AbstractUser>();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void AddUser(AbstractUser user)
        {
            LinqProblems.EnsureIsNotNull(user, nameof(user));
            users.Add(user);
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

            SendNotification(list[productIndex], StockWarning);
        }

        void SendNotification(Product product, Func<string, int, string> callback)
        {
            LinqProblems.EnsureIsNotNull(product, nameof(product));
            LinqProblems.EnsureIsNotNull(callback, nameof(callback));

            var warning = callback(product.Name, product.Quantity);
            foreach (var user in users)
            {
                user.ReceiveNotification(warning);
            }
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
