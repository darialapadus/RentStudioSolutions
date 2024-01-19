using RentStudio.DataAccesLayer;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models
{
    public class HotelDTO
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        public float Rating { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = "";
    }
    public class GroupedHotelsByRatingDTO
    {
        public float Rating { get; set; }
        public IEnumerable<HotelDTO> Hotels { get; set; }
    }
    public class HotelWithRoomsDTO
    {
        public HotelDTO Hotel { get; set; }
        public IEnumerable<RoomDTO> Rooms { get; set; }
    }

}
