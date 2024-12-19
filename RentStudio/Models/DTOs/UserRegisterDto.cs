using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public string? CNP { get; set; }
    }
}
