using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int Age { get; set; }
        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        public double Grade { get; set; }
    }
}
