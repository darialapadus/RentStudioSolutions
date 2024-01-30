using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class RoomTypeDTO
    {
        public int RoomTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public required string Facilities { get; set; }

        [Required]
        public decimal Price { get; set; }
    }

    public class RoomTypeWithRoomsDTO
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public string Facilities { get; set; }
        public decimal Price { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }
}
