using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class RoomController : BaseController
    {
        private readonly RentDbContext _context;

        public RoomController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms.ToList();
            return Ok(rooms);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela rooms);HttpReq e format din Header si Body
        public IActionResult AddRooms([FromBody] RoomDTO room)
        {
            var entity = new Room();
            entity.Number = room.Number;
            entity.RoomTypeId = room.RoomTypeId;
            entity.HotelId = room.HotelId;
            _context.Rooms.Add(entity);
            _context.SaveChanges();
            return Ok(room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomShortDTO updatedRoom)
        {
            var existingRoom = _context.Rooms.Find(id);
            if (existingRoom == null)
                return NotFound();

            existingRoom.Number = updatedRoom.Number;

            _context.SaveChanges();
            return Ok(existingRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return Ok(room);
        }

        // GROUPBY pentru a grupa camerele in functie de numar si pentru a sorta rezultatele crescator.
        [HttpGet("rooms/grouped-by-number")]
        public IActionResult GetRoomsGroupedByNumber()
        {
            var roomsGroupedByNumber = _context.Rooms
                .GroupBy(r => r.Number)
                .OrderBy(group => group.Key) // Sortare crescatoare dupa cheie (numar)
                .Select(group => new
                {
                    Number = group.Key,
                    Rooms = group.ToList()
                })
                .ToList();

            return Ok(roomsGroupedByNumber);
        }

        //WHERE pentru a obtine toate camerele de un anumit tip.
        [HttpGet("rooms-of-type/{roomTypeId}")]
        public IActionResult GetRoomsOfType(int roomTypeId)
        {
            var roomsOfType = _context.Rooms
                .Where(r => r.RoomTypeId == roomTypeId)
                .ToList();

            return Ok(roomsOfType);
        }

        //JOIN intre Rooms si Hotels pentru a obtine informatiile despre camere impreuna cu datele despre hoteluri.
        [HttpGet("rooms-with-hotels")]
        public IActionResult GetRoomsWithHotels()
        {
            var roomsWithHotels = _context.Rooms
                .Select(room => new
                {
                    Room = new RoomDTO
                    {
                        RoomId = room.RoomId,
                        Number = room.Number,
                        RoomTypeId = room.RoomTypeId,
                        HotelId = room.HotelId
                    },
                    Hotel = new HotelDTO
                    {
                        HotelId = room.Hotel.HotelId,
                        Name = room.Hotel.Name,
                        Rating = room.Hotel.Rating,
                        Address = room.Hotel.Address
                    }
                })
                .ToList();

            return Ok(roomsWithHotels);
        }

        //INCLUDE pentru a incarca toate detaliile despre camere impreuna cu informatiile despre rezervarile existente.
        [HttpGet("rooms-with-reservations")]
        public IActionResult GetRoomsWithReservations()
        {
            var roomsWithReservations = _context.Rooms
                .Include(r => r.BookedRooms)
                .ToList();

            return Ok(roomsWithReservations);
        }

    }
}

