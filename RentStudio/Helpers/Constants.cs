namespace RentStudio.Helpers
{
    public class Constants
    {
        public static class ErrorMessages
        {
            public const string PaymentNotFound = "Payment not found";
            public const string PaymentAlreadyExists = "Payment already exists";
            public const string PaymentNotSaved = "Payment not saved";
            public const string PaymentNotProcessed = "Payment not processed";
            public const string PaymentStatusNotChecked = "Payment status not checked";
        }

        public static class PaymentMessage
        {
            public const string NoPaymentFound = "No payment has been made for this reservation!";
            public const string RefundAlreadyProcessed = "The refund has already been processed!";
            public const string RefundProcessed = "The refund has been successfully recorded!";
        }
    }
}
