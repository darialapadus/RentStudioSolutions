namespace RentStudio.Models.DTOs
{
    public class PaymentDetailsDTO
    {
        public int NumberOfRooms { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
