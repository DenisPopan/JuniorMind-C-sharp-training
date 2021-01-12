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
            void Callback(Product product)
            {
                returnedProduct = product; 
            }

            var stock = new Stock(Callback);
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
            void Callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(Callback);

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
            void Callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(Callback);
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
            void Callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(Callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.Sell("Phone", 340);
            Assert.Equal(6, stock.ProductQuantity("Phone"));
        }

        [Fact]

        public void SellMethodShouldReturnAProductThroughCallbackOnlyWhenQuantityIsBelowACertainLevel()
        {
            Product returnedProduct = null;
            void Callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(Callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.Sell("Phone", 300);
            Assert.Null(returnedProduct);

            stock.Sell("Phone", 40);
            Assert.Equal(6, stock.ProductQuantity("Phone"));
            Assert.Equal("Phone", returnedProduct.Name);

            Assert.Throws<ArgumentException>(() => stock.Sell("Phone", 34));
        }

        [Fact]

        public void ANotificationShouldNotBeSentMultipleTimesIfAProductsQuantityIsStillBelowTheSameLevel()
        {
            Product returnedProduct = null;
            void Callback(Product product)
            {
                returnedProduct = product;
            }

            var stock = new Stock(Callback);
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.Sell("Phone", 337);
            Assert.Equal(9, stock.ProductQuantity("Phone"));
            Assert.Equal("Phone", returnedProduct.Name);

            returnedProduct = null;
            stock.Sell("Phone", 1);
            Assert.Null(returnedProduct);

            stock.Sell("Phone", 4);
            Assert.Equal("Phone", returnedProduct.Name);

            returnedProduct = null;
            stock.Sell("Phone", 1);
            Assert.Null(returnedProduct);

            stock.Sell("Phone", 2);
            Assert.Equal("Phone", returnedProduct.Name);
        }
    }
}
