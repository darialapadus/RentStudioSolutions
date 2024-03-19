using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class EmployeeRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        public decimal BaseSalary { get; set; }
        
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
