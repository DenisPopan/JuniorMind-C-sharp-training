namespace Linq
{
    public abstract class AbstractUser
    {
        protected Stock stock;

        public abstract string ReceiveNotification(string notification);
    }
}
