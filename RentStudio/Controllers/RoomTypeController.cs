using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

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

        //GROUPBY pentru a grupa tipurile de camere in functie de nume.
        [HttpGet("roomtypes/grouped-by-name")]
        public IActionResult GetRoomTypesGroupedByName()
        {
            var roomTypesGroupedByName = _context.RoomTypes
                .GroupBy(rt => rt.Name)
                .Select(group => new
                {
                    Name = group.Key,
                    RoomTypes = group.ToList()
                })
                .ToList();

            return Ok(roomTypesGroupedByName);
        }

        //WHERE pentru a obtine toate tipurile de camere care au facilitati specifice.
        [HttpGet("roomtypes-with-facilities/{facilities}")]
        public IActionResult GetRoomTypesWithFacilities(string facilities)
        {
            var roomTypesWithFacilities = _context.RoomTypes
                .Where(rt => rt.Facilities.Contains(facilities))
                .ToList();

            return Ok(roomTypesWithFacilities);
        }

        //JOIN intre RoomTypes si Rooms pentru a obtine informatiile despre tipurile de camere impreuna cu datele despre camere.
        [HttpGet("roomtypes-with-rooms")]
        public IActionResult GetRoomTypesWithRooms()
        {
            var roomTypesWithRooms = _context.RoomTypes
                .Join(
                    _context.Rooms,
                    roomType => roomType.RoomTypeId,
                    room => room.RoomTypeId,
                    (roomType, room) => new
                    {
                        RoomTypeId = roomType.RoomTypeId,
                        Name = roomType.Name,
                        Facilities = roomType.Facilities,
                        Price = roomType.Price,
                        Room = new RoomDTO
                        {
                            RoomId = room.RoomId,
                            Number = room.Number,
                            RoomTypeId = room.RoomTypeId,
                            HotelId = room.HotelId
                        }
                    })
                .ToList();

            return Ok(roomTypesWithRooms);
        }


        //INCLUDE pentru a incarca toate detaliile despre tipurile de camere impreuna cu informatiile despre camerele existente.
        [HttpGet("roomtypes-with-rooms-details")]
        public IActionResult GetRoomTypesWithRoomsDetails()
        {
            var roomTypesWithRoomsDetails = _context.RoomTypes
                .Include(rt => rt.Rooms)
                .Select(rt => new
                {
                    RoomType = new RoomTypeDTO
                    {
                        RoomTypeId = rt.RoomTypeId,
                        Name = rt.Name,
                        Facilities = rt.Facilities,
                        Price = rt.Price
                    },
                    Rooms = rt.Rooms.Select(room => new RoomDTO
                    {
                        RoomId = room.RoomId,
                        Number = room.Number,
                        RoomTypeId = room.RoomTypeId,
                        HotelId = room.HotelId
                    }).ToList()
                })
                .ToList();

            return Ok(roomTypesWithRoomsDetails);
        }


    }
}
