using Microsoft.AspNetCore.Mvc;
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
    }
}

