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
            var stock = new Stock();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 36);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal(4, stock.Count);

            Assert.Throws<ArgumentException>(() => stock.AddProduct("Laptop", 34));
        }

        [Fact]

        public void StatusMethodShouldReturnAllProductsAndTheirCurrentQuantity()
        {
            var stock = new Stock();
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
        }

        [Fact]

        public void ProductQuantityMethodShouldReturnAProductsQuantity()
        {
            var stock = new Stock();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 2);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Equal(6574, stock.ProductQuantity("Camera"));
        }

        [Fact]

        public void SellMethodShouldReduceAProductsQuantity()
        {
            ProductEventArgs returnedProduct = null;
            void Callback(object sender, ProductEventArgs product)
            {
                returnedProduct = product;
            }

            var stock = new Stock();
            stock.ItemsSold += Callback;
            //stock.Callback = Callback;
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
            ProductEventArgs returnedProduct = null;
            void Callback(object sender, ProductEventArgs product)
            {
                returnedProduct = product;
            }

            var stock = new Stock();
            stock.ItemsSold += Callback;
            //stock.Callback = Callback;
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

        public void ANotificationShouldNotBeSentIfTheUserIsNotRegistered()
        {
            var stock = new Stock();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            Assert.Throws<ArgumentNullException>(() => stock.Sell("Phone", 340));
        }

        [Fact]

        public void ANotificationShouldNotBeSentMultipleTimesIfAProductsQuantityIsStillBelowTheSameLevel()
        {
            ProductEventArgs returnedProduct = null;
            void Callback(object sender, ProductEventArgs product)
            {
                returnedProduct = product;
            }

            var stock = new Stock();
            stock.ItemsSold += Callback;
            //stock.Callback = Callback;
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
