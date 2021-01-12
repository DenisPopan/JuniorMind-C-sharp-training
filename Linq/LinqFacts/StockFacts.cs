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
            Product returnedProduct = null;
            void callback(Product product)
            {
                returnedProduct = product; 
            }

            var stock = new Stock(callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 36);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal(4, stock.Count);

            Assert.Null(returnedProduct);

            Assert.Throws<ArgumentException>(() => stock.AddProduct("Laptop", 34));
        }

        [Fact]

        public void StatusMethodShouldReturnAllProductsAndTheirCurrentQuantity()
        {
            Product returnedProduct = null;
            void callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(callback);

            Assert.Equal("", stock.Status());

            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            var status = stock.Status();

            Assert.Equal("Product: Phone, Quantity: 346\n" +
                "Product: Tablet, Quantity: 2\n" +
                "Product: Camera, Quantity: 6574\n" +
                "Product: Laptop, Quantity: 3346\n", stock.Status());

            Assert.Null(returnedProduct);
        }

        [Fact]

        public void ProductQuantityMethodShouldReturnAProductsQuantity()
        {
            Product returnedProduct = null;
            void callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal(6574, stock.ProductQuantity("Camera"));

            Assert.Null(returnedProduct);
        }

        [Fact]

        public void SellMethodShouldReduceAProductsQuantity()
        {
            Product returnedProduct = null;
            void callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.Sell("Phone", 340);
            Assert.Equal(6, stock.ProductQuantity("Phone"));
            Assert.Throws<ArgumentException>(() => stock.Sell("Phone", 34));
        }
    }
}
