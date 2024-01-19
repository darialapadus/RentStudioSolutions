using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; } = "";

        public int Age { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; } = "";

        public decimal Salary { get; set; }

        public int HotelId { get; set; }
    }
    public class GroupedEmployeesDTO
    {
        public string Position { get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }


}
