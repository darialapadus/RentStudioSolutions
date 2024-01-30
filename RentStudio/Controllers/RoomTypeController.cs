using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Services.RoomService;
using RentStudio.Services.RoomTypeService;

namespace RentStudio.Controllers
{
    public class RoomTypeController : BaseController
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        public IActionResult GetRoomTypes()
        {
            var roomTypes = _roomTypeService.GetRoomTypes();
            return Ok(roomTypes);
        }

        [HttpPost]
        public IActionResult AddRoomTypes([FromBody] RoomTypeDTO roomTypeDto)
        {
            _roomTypeService.AddRoomTypes(roomTypeDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoomType(int id, [FromBody] RoomTypeShortDTO updatedRoomType)
        {
            _roomTypeService.UpdateRoomType(id, updatedRoomType);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoomType(int id)
        {
            _roomTypeService.DeleteRoomType(id);
            return Ok();
        }

        //GROUPBY pentru a grupa tipurile de camere in functie de nume.
        [HttpGet("roomtypes/grouped-by-name")]
        public IActionResult GetRoomTypesGroupedByName()
        {
            var roomTypesGroupedByName = _roomTypeService.GetRoomTypesGroupedByName();
            return Ok(roomTypesGroupedByName);
        }

        //WHERE pentru a obtine toate tipurile de camere care au facilitati specifice.
        [HttpGet("with-facilities/{facilities}")]
        public IActionResult GetRoomTypesWithFacilities(string facilities)
        {
            var roomTypesWithFacilities = _roomTypeService.GetRoomTypesWithFacilities(facilities);
            return Ok(roomTypesWithFacilities);
        }

        //JOIN intre RoomTypes si Rooms pentru a obtine informatiile despre tipurile de camere impreuna cu datele despre camere.
        [HttpGet("roomtypes-with-rooms")]
        public IActionResult GetRoomTypesWithRooms()
        {
            var roomTypesWithRooms = _roomTypeService.GetRoomTypesWithRooms();
            return Ok(roomTypesWithRooms);
        }

    }
}
