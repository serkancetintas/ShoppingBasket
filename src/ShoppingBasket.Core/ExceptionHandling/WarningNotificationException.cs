using System;

namespace ShoppingBasket.Core.ExceptionHandling
{
    public class WarningNotificationException : NotificationException
    {
        public WarningNotificationException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public WarningNotificationException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public override string NotifyType => "warning";
    }
}
