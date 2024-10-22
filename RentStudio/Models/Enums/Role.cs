namespace RentStudio.Models.Enums
{
    public enum Role
    {
        Admin,
        User
    }

    public enum EmployeeRole
    {
        Manager = 1,
        Receptionist = 2,
        Cleaner = 3,
        Security = 4
    }

    public enum Taxes
    {
        SalaryTax = 45
    }

    public enum PaymentMethod
    {
        Cash = 0,
        Card = 1,
        BankTransfer = 2
    }

    public enum PaymentStatus
    {
        Succeeded = 0,
        Failed = 1,
        InsufficientFunds = 2,
        Processed = 3,
        Refund = 4,
        Pending = 5,
    }
}
