namespace Linq
{
    public class StockUser
    {
        readonly Stock stock;

        public StockUser(Stock stock)
        {
            this.stock = stock;
            this.stock.AddUser(this);
        }

        public string ReceiveNotification(string notification)
        {
            LinqProblems.EnsureIsNotNull(notification, nameof(notification));
            return notification;
        }
    }
}