using Microsoft.AspNetCore.Mvc;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class RoomTypeController : BaseController
    {
        private readonly RentDbContext _context;

        public RoomTypeController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetRoomTypes()
        {
            var roomTypes = _context.RoomTypes.ToList();
            return Ok(roomTypes);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela roomTypes);HttpReq e format din Header si Body
        public IActionResult AddRoomTypes([FromBody] RoomTypeDTO roomType)
        {
            var entity = new RoomType();
            entity.Name = roomType.Name;
            entity.Facilities = roomType.Facilities;
            entity.Price = roomType.Price;
            _context.RoomTypes.Add(entity);
            _context.SaveChanges();
            return Ok(roomType);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoomType(int id, [FromBody] RoomTypeShortDTO updatedRoomType)
        {
            var existingRoomType = _context.RoomTypes.Find(id);
            if (existingRoomType == null)
                return NotFound();

            existingRoomType.Facilities = updatedRoomType.Facilities;
            existingRoomType.Price = updatedRoomType.Price;

            _context.SaveChanges();
            return Ok(existingRoomType);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoomType(int id)
        {
            var roomType = _context.RoomTypes.Find(id);
            if (roomType == null)
                return NotFound();

            _context.RoomTypes.Remove(roomType);
            _context.SaveChanges();
            return Ok(roomType);
        }
    }
}
