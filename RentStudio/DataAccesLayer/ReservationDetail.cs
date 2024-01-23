using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class ReservationDetail
    {
        [Key]
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; } 
        public virtual Reservation Reservation { get; set; }

        public string? SpecialRequests { get; set; }
        public DateTime? LastModified { get; set; }
        public string? BillingInformation { get; set; }
    }
}
