using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class FilterRoomTypeDTO
    {
        public string Name { get; set; } = "";

        public string Facilities { get; set; } ="";

        public decimal Price { get; set; }
    }
}
