using System;

namespace ShoppingBasket.Core.ExceptionHandling
{
    public abstract class NotificationException:Exception
    {
        protected NotificationException(string message)
            :base(message)
        {

        }

        protected NotificationException(string message, Exception innerException)
            :base(message,innerException)
        {

        }

        public string ErrorCode { get; protected set; }
        public string Parameters { get; protected set; }
        public abstract string NotifyType { get; }
    }
}
