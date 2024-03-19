using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class FilterCustomerDTO
    {
        public string? LastName { get; set; } = "";

        public string? Email { get; set; } = "";

        public string? Phone { get; set; } = "";

        public string? City { get; set; } = "";
    }
}
