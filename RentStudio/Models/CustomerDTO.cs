using RentStudio.DataAccesLayer;
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
    public class GroupedCustomersDTO
    {
        public string City { get; set; }
        public List<Customer> Customers { get; set; }
    }

    public class CustomerWithReservationsDTO
    {
        public CustomerDTO Customer { get; set; }
        public ReservationDTO Reservation { get; set; }
    }

}
