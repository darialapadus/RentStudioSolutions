using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class RoomTypeShortDTO
    {
        [MaxLength(200)]
        public required string Facilities { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
