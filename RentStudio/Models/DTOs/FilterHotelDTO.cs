using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class FilterHotelDTO
    {
        public float Rating { get; set; }

        public string Address { get; set; } = "";
    }
}
