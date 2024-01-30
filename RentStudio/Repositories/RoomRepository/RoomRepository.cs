using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace RentStudio.Repositories.RoomRepository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RentDbContext _context;

        public RoomRepository(RentDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

        public void AddRoom(RoomDTO roomDto)
        {
            var entity = new Room
            {
                Number = roomDto.Number,
                RoomTypeId = roomDto.RoomTypeId,
                HotelId = roomDto.HotelId
            };

            _context.Rooms.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateRoom(int id, RoomShortDTO updatedRoom)
        {
            var existingRoom = _context.Rooms.Find(id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException("Room not found");
            }

            existingRoom.Number = updatedRoom.Number;

            _context.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RoomGroupedDTO> GetRoomsGroupedByNumber()
        {
            return _context.Rooms
                .GroupBy(r => r.Number)
                .OrderBy(group => group.Key)
                .Select(group => new RoomGroupedDTO
                {
                    Number = group.Key,
                    Rooms = group.Select(room => new RoomDTO
                    {
                        RoomId = room.RoomId,
                        Number = room.Number
                    }).ToList()
                })
                .ToList();
        }

        public IEnumerable<Room> GetRoomsOfType(int roomTypeId)
        {
            return _context.Rooms
                .Where(r => r.RoomTypeId == roomTypeId)
                .ToList();
        }

        public IEnumerable<Room> GetRoomsWithHotels()
        {
            return _context.Rooms
                .Include(r => r.Hotel) 
                .ToList();
        }

        public IEnumerable<Room> GetRoomsWithReservations()
        {
            return _context.Rooms.Include(r => r.BookedRooms).ToList();
        }
    }
}
