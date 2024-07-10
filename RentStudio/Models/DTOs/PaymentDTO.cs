using RentStudio.Models.Enums;

namespace RentStudio.Models.DTOs
{
    public class PaymentDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CNP { get; set; } 
        public string Address { get; set; }
        public Guid? UserId { get; set; } 

        public string CardNumberOrIBAN { get; set; }
        public string CVV { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public string BankName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }

        public int ReservationId { get; set; }

        public string Email { get; set; }

    }
}
