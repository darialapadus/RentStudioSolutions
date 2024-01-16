using RentStudio.DataAccesLayer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models
{
    public class RoomDTO
    {
        [Key]
        public int RoomId { get; set; }
        public string Number { get; set; } = "";

        [ForeignKey("RoomTypeId")]
        public int RoomTypeId { get; set; }

        [ForeignKey("HotelId")]
        public int HotelId { get; set; }
    }
}
