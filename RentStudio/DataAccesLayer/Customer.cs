using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(15)]
        public string Phone { get; set; } = "";

        [Required]
        [MaxLength(50)]
        public string City { get; set; } = "";
    }

}
