using System;
using System.Collections.Generic;

namespace Linq
{
    public class Stock
    {
        readonly List<ProductEventArgs> list;

        public Stock()
        {
            list = new List<ProductEventArgs>();
        }

        public event EventHandler<ProductEventArgs> ItemsSold;

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

            list.Add(new ProductEventArgs { Name = name, Quantity = quantity });
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

        public void Sell(string name, int quantity)
        {
            LinqProblems.EnsureIsNotNull(name, nameof(name));
            LinqProblems.EnsureIsNotNull(quantity, nameof(quantity));
            LinqProblems.EnsureIsNotNull(ItemsSold, nameof(ItemsSold));

            ProductExists(name);
            var productIndex = FindProductIndex(name);

            var previousProductQuantity = list[productIndex].Quantity;

            IsCurrentQuantityNotEnough(previousProductQuantity, quantity);

            list[productIndex].Quantity -= quantity;

            OnItemsSold(list[productIndex], previousProductQuantity);
        }

        protected virtual void OnItemsSold(ProductEventArgs product, int previousProductQuantity)
        {
            LinqProblems.EnsureIsNotNull(product, nameof(product));

            if (product.Quantity > 9)
            {
                return;
            }

            var levels = new[] { 2, 5, 10 };

            var level = levels.First(x => product.Quantity < x);

            if (previousProductQuantity < level)
            {
                return;
            }

            ItemsSold.Invoke(this, product);
        }

        private void IsCurrentQuantityNotEnough(int previousProductQuantity, int quantityToSell)
        {
            if (previousProductQuantity >= quantityToSell)
            {
                return;
            }

            throw new ArgumentException($"You can only sell {previousProductQuantity} items!");
        }

        int FindProductIndex(string name)
        {
            return list.FindIndex(product => string.Equals(product.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        void ProductExists(string name)
        {
            if (list.Any(product => product.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            throw new ArgumentException("Product not found.");
        }
    }
}
