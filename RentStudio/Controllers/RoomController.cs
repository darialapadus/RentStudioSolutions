using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Services.RoomService;

namespace RentStudio.Controllers
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomService.GetRooms();
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult AddRoom([FromBody] RoomDTO roomDto)
        {
            _roomService.AddRoom(roomDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomShortDTO updatedRoom)
        {
            _roomService.UpdateRoom(id, updatedRoom);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            _roomService.DeleteRoom(id);
            return Ok();
        }

        // GROUPBY pentru a grupa camerele in functie de numar si pentru a sorta rezultatele crescator.
        [HttpGet("rooms/grouped-by-number")]
        public IActionResult GetRoomsGroupedByNumber()
        {
            var roomsGroupedByNumber = _roomService.GetRoomsGroupedByNumber();
            return Ok(roomsGroupedByNumber);
        }

        //WHERE pentru a obtine toate camerele de un anumit tip.
        [HttpGet("rooms-of-type/{roomTypeId}")]
        public IActionResult GetRoomsOfType(int roomTypeId)
        {
            var roomsOfType = _roomService.GetRoomsOfType(roomTypeId);
            return Ok(roomsOfType);
        }

        //JOIN intre Rooms si Hotels pentru a obtine informatiile despre camere impreuna cu datele despre hoteluri.
        [HttpGet("rooms-with-hotels")]
        public IActionResult GetRoomsWithHotels()
        {
            var roomsWithHotels = _roomService.GetRoomsWithHotels();
            return Ok(roomsWithHotels);
        }

        //INCLUDE pentru a incarca toate detaliile despre camere impreuna cu informatiile despre rezervarile existente.
        [HttpGet("rooms-with-reservations")]
        public IActionResult GetRoomsWithReservations()
        {
            var roomsWithReservations = _roomService.GetRoomsWithReservations();
            return Ok(roomsWithReservations);
        }

    }
}

