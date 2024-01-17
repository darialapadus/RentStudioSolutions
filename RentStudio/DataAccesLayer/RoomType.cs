using System.ComponentModel.DataAnnotations;

namespace RentStudio.DataAccesLayer
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Facilities { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; } //add after
    }

}
