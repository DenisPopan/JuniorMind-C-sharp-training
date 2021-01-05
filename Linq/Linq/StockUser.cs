namespace Linq
{
    public class StockUser : AbstractUser
    {
        public StockUser(Stock stock)
        {
            this.stock = stock;
            this.stock.AddUser(this);
        }

        public override string ReceiveNotification(string notification)
        {
            LinqProblems.EnsureIsNotNull(notification, nameof(notification));
            return notification;
        }
    }
}