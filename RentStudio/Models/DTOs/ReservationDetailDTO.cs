using System.ComponentModel.DataAnnotations.Schema;

namespace RentStudio.Models.DTOs
{
    public class ReservationDetailDTO
    {
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }
        public string? SpecialRequests { get; set; }
        public DateTime? LastModified { get; set; }
        public string? BillingInformation { get; set; }
    }
    public class ReservationDetailGroupedByRequestsDTO
    {
        public string SpecialRequests { get; set; }
        public List<ReservationDetailDTO> ReservationDetails { get; set; }
    }
}
