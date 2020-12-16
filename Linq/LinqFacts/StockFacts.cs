using System;
using Xunit;
using Linq;
namespace LinqFacts
{
    public class StockFacts
    {
        [Fact]

        public void AddProductMethodShouldAddNewProductToCurrentStock()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 36);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal(4, stock.Count);

            Assert.Throws<ArgumentException>(() => stock.AddProduct("Laptop", 34));
        }

        [Fact]

        public void RemoveProductMethodShouldRemoveAGivenProduct()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 36);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.RemoveProduct("Laptop");

            Assert.Throws<ArgumentException>(() => stock.ProductQuantity("Laptop"));
        }

        [Fact]

        public void StatusMethodShouldReturnAllProductsAndTheirCurrentQuantity()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            var status = stock.Status();

            Assert.Equal("Product: Phone, Quantity: 346\n" +
                "Product: Tablet, Quantity: Product stock has less than 5 items!(2)\n" +
                "Product: Camera, Quantity: 6574\n" +
                "Product: Laptop, Quantity: 3346\n", stock.Status());
        }

        [Fact]

        public void ProductQuantityMethodShouldReturnAProductsQuantity()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal("6574", stock.ProductQuantity("Camera"));
            Assert.Equal("Product stock has less than 5 items!(2)", stock.ProductQuantity("Tablet"));
        }

        [Fact]

        public void BuyNewStockMethodShouldAddNewQuantityToAGivenProduct()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.BuyNewStock("Laptop", 200);

            Assert.Equal("3546", stock.ProductQuantity("Laptop"));
        }

        [Fact]

        public void SellProductStockMethodShouldReduceAProductsQuantityAndReturnTheNewOne()
        {
            var stock = new Stock<Product>();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal("Product stock has less than 10 items!(6)", stock.SellProductStock("Phone", 340));
            Assert.Equal("12", stock.SellProductStock("Tablet", 10));
        }
    }
}
