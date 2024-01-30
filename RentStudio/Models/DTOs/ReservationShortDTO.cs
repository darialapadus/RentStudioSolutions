using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class ReservationShortDTO
    {
        [Required]
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
