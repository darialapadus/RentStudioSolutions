using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class EmployeeShortDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string Position { get; set; } = "";
    }
}
