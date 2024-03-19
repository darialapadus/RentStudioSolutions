using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class FilterEmployeeDTO
    {
        public string? FirstName { get; set; }

        public string? Gender { get; set; }

        public string? Position { get; set; }

        public decimal Salary { get; set; }

    }
}