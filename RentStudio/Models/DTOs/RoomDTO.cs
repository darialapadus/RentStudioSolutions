using RentStudio.DataAccesLayer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
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

        public HotelDTO Hotel { get; set; } 

    }

    public class RoomGroupedDTO
    {
        public string Number { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }
}
