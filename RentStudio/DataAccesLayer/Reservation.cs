using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }

        [Required]
        public string Status { get; set; } = "";

        public string PaymentMethod { get; set; } = "";

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<BookedRoom> BookedRooms { get; set; }

    }
}
