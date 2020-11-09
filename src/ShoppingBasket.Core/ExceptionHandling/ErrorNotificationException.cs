using System;

namespace ShoppingBasket.Core.ExceptionHandling
{
    public class ErrorNotificationException : NotificationException
    {
        public ErrorNotificationException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorNotificationException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public override string NotifyType => "error";
    }
}
