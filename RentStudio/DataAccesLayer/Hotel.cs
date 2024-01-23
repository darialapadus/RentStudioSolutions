using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        public float Rating { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = "";

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
