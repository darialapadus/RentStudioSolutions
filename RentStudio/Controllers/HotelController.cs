using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GROUPBY pentru a grupa hotelurile in functie de rating.
        [HttpGet("hotels/grouped-by-rating")]
        public IActionResult GetHotelsGroupedByRating()
        {
            var hotelsGroupedByRating = _context.Hotels
                .GroupBy(h => h.Rating)
                .Select(group => new
                {
                    Rating = group.Key,
                    Hotels = group.ToList()
                })
                .ToList();

            return Ok(hotelsGroupedByRating);
        }

        // WHERE pentru a obtine toate hotelurile cu o anumita adresa.
        [HttpGet("hotels-with-address/{address}")]
        public IActionResult GetHotelsWithAddress(string address)
        {
            var hotelsWithAddress = _context.Hotels
                .Where(h => h.Address.Contains(address))
                .ToList();

            return Ok(hotelsWithAddress);
        }

        // JOIN intre Hotels si Rooms pentru a obtine informatiile despre hoteluri impreuna cu datele despre camere.
        [HttpGet("hotels-with-rooms")]
        public IActionResult GetHotelsWithRooms()
        {
            var hotelsWithRooms = _context.Hotels
                .Join(
                    _context.Rooms,
                    hotel => hotel.HotelId,
                    room => room.HotelId,
                    (hotel, room) => new
                    {
                        Hotel = new HotelDTO
                        {
                            HotelId = hotel.HotelId,
                            Name = hotel.Name,
                            Rating = hotel.Rating,
                            Address = hotel.Address
                        },
                        Room = new RoomDTO
                        {
                            RoomId = room.RoomId,
                            Number = room.Number,
                            RoomTypeId = room.RoomTypeId,
                            HotelId = room.HotelId
                        }
                    })
                .ToList();

            return Ok(hotelsWithRooms);
        }


        // INCLUDE pentru a incarca toate camerele unui hotel impreuna cu informatiile despre hotel.
        [HttpGet("hotels-with-rooms-and-details")]
        public IActionResult GetHotelsWithRoomsAndDetails()
        {
            var hotelsWithRoomsAndDetails = _context.Hotels
                .Include(h => h.Rooms)
                .Select(h => new
                {
                    Hotel = new HotelDTO
                    {
                        HotelId = h.HotelId,
                        Name = h.Name,
                        Rating = h.Rating,
                        Address = h.Address
                    },
                    Rooms = h.Rooms.Select(r => new RoomDTO
                    {
                        RoomId = r.RoomId,
                        Number = r.Number,
                        RoomTypeId = r.RoomTypeId,
                        HotelId = r.HotelId
                    }).ToList()
                })
                .ToList();

            return Ok(hotelsWithRoomsAndDetails);
        }


    }
}
