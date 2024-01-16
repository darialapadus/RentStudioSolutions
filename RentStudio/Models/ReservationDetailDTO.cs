using System.ComponentModel.DataAnnotations.Schema;

namespace RentStudio.Models
{
    public class ReservationDetailDTO
    {
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public string? SpecialRequests { get; set; }
        public DateTime? LastModified { get; set; }
        public string? BillingInformation { get; set; }
    }
}
