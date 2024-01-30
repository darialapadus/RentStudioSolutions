using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class CustomerShortDTO
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(15)]
        public string Phone { get; set; } = "";
    }
}
