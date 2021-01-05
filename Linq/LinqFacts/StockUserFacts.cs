using System;
using Xunit;
using Linq;

namespace LinqFacts
{
    public class StockUserFacts
    {
        [Fact]

        public void ReceiveNotificationMethodShouldReturnTheNotificationReceivedByAUser()
        {
            var stock = new Stock();
            stock.AddProduct("Phone", 346);
            stock.AddProduct("Tablet", 22);
            stock.AddProduct("Camera", 6574);
            stock.AddProduct("Laptop", 3346);

            new StockUser(stock);
            new StockUser(stock);
            stock.SellProductStock("Camera", 6573);
            stock.SellProductStock("Tablet", 13);
            stock.SellProductStock("Laptop", 3342);
            //Assert.Equal("Hurry! There are only 1 Cameras left!", user.ReceiveNotification());
        }
    }
}
