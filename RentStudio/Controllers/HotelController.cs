using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;
using RentStudio.Services;

namespace RentStudio.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _hotelService.GetHotels();
            return Ok(hotels);
        }
        [HttpPost]
        public IActionResult AddHotel([FromBody] HotelDTO hotelDto)
        {
            _hotelService.AddHotel(hotelDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            _hotelService.DeleteHotel(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] HotelShortDTO updatedHotel)
        {
            _hotelService.UpdateHotel(id, updatedHotel);
            return Ok();
        }

        [HttpGet("hotels/grouped-by-rating")]
        public IActionResult GetHotelsGroupedByRating()
        {
            var hotelsGroupedByRating = _hotelService.GetHotelsGroupedByRating();
            return Ok(hotelsGroupedByRating);
        }
        [HttpGet("hotels-with-address/{address}")]
        public IActionResult GetHotelsWithAddress(string address)
        {
            var hotelsWithAddress = _hotelService.GetHotelsWithAddress(address);
            return Ok(hotelsWithAddress);
        }
        [HttpGet("hotels-with-rooms")]
        public IActionResult GetHotelsWithRooms()
        {
            var hotelsWithRooms = _hotelService.GetHotelsWithRooms();
            return Ok(hotelsWithRooms);
        }
        
    }
}
