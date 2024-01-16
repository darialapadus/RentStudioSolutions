using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models
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
}
