using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Services.HotelService;

namespace RentStudio.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        /*[HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _hotelService.GetHotels();
            return Ok(hotels);
        }*/

        [HttpGet]
        public IActionResult GetHotels([FromQuery] FilterHotelDTO filterHotelDTO)
        {
            var hotels = _hotelService.GetHotels(filterHotelDTO);
            return Ok(hotels);
        }

        [HttpPost]
        public IActionResult AddHotel([FromBody] HotelDTO hotelDto)
        {
            _hotelService.AddHotel(hotelDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] HotelShortDTO updatedHotel)
        {
            _hotelService.UpdateHotel(id, updatedHotel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            _hotelService.DeleteHotel(id);
            return Ok();
        }

        // GROUPBY pentru a grupa hotelurile in functie de rating.
        [HttpGet("hotels/grouped-by-rating")]
        public IActionResult GetHotelsGroupedByRating()
        {
            var hotelsGroupedByRating = _hotelService.GetHotelsGroupedByRating();
            return Ok(hotelsGroupedByRating);
        }

        // WHERE pentru a obtine toate hotelurile cu o anumita adresa.
        [HttpGet("hotels-with-address/{address}")]
        public IActionResult GetHotelsWithAddress(string address)
        {
            var hotelsWithAddress = _hotelService.GetHotelsWithAddress(address);
            return Ok(hotelsWithAddress);
        }

        // JOIN intre Hotels si Rooms pentru a obtine informatiile despre hoteluri impreuna cu datele despre camere.
        [HttpGet("hotels-with-rooms")]
        public IActionResult GetHotelsWithRooms()
        {
            var hotelsWithRooms = _hotelService.GetHotelsWithRooms();
            return Ok(hotelsWithRooms);
        }

    }
}