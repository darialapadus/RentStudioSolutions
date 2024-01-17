using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models
{
    public class CustomerDTO
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

        public List<ReservationDTO> Reservations { get; set; }

    }
}
