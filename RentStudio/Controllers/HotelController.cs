using Microsoft.AspNetCore.Mvc;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class HotelController : BaseController
    {
        private readonly RentDbContext _context;

        public HotelController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetHotels()
        {
            var hotels = _context.Hotels.ToList(); 
            return Ok(hotels);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela hotels);HttpReq e format din Header si Body
        public IActionResult AddHotels([FromBody] HotelDTO hotel)
        {
            var entity = new Hotel();
            entity.Name = hotel.Name;
            entity.Rating= hotel.Rating;
            entity.Address = hotel.Address;
            _context.Hotels.Add(entity);
            _context.SaveChanges(); 
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] HotelShortDTO updatedHotel)
        {
            var existingHotel = _context.Hotels.Find(id);
            if (existingHotel == null)
                return NotFound();

            existingHotel.Rating = updatedHotel.Rating;

            _context.SaveChanges();
            return Ok(existingHotel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel == null)
                return NotFound();

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
            return Ok(hotel);
        }
    }
}
