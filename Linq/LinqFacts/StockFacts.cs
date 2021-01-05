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

            Assert.Equal("There are 6574 items of type Camera in stock", stock.ProductQuantity("Camera"));
        }

        [Fact]

        public void SellProductStockMethodShouldReduceAProductsQuantityAndReturnTheNewOne()
        {
            var stock = new Stock();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            stock.SellProductStock("Phone", 340);
            Assert.Equal("There are 6 items of type Phone in stock", stock.ProductQuantity("Phone"));
            Assert.Throws<ArgumentException>(() => stock.SellProductStock("Phone", 34));
        }
    }
}
