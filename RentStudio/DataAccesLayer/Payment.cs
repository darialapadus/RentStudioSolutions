using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

        public decimal Amount { get; set; } 
        public string Status { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
