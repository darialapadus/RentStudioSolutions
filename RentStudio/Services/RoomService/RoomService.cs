using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Repositories.RoomRepository;

namespace RentStudio.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public bool Save()
        {
            return _roomRepository.Save();
        }

        public IEnumerable<RoomDTO> GetRooms()
        {
            var rooms = _roomRepository.GetRooms();
            return rooms.Select(r => new RoomDTO
            {
                RoomId = r.RoomId,
                Number = r.Number,
                RoomTypeId = r.RoomTypeId,
                HotelId = r.HotelId,
               
            }).ToList();
        }

        public void AddRoom(RoomDTO roomDto)
        {
            _roomRepository.AddRoom(roomDto);
        }

        public void UpdateRoom(int id, RoomShortDTO updatedRoom)
        {
            _roomRepository.UpdateRoom(id, updatedRoom);
        }

        public void DeleteRoom(int id)
        {
            _roomRepository.DeleteRoom(id);
        }

        public IEnumerable<RoomGroupedDTO> GetRoomsGroupedByNumber()
        {
            return _roomRepository.GetRoomsGroupedByNumber();
        }

        public IEnumerable<RoomDTO> GetRoomsOfType(int roomTypeId)
        {
            var roomsOfType = _roomRepository.GetRoomsOfType(roomTypeId);

            var roomsOfTypeDTO = roomsOfType.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                Number = room.Number,
            }).ToList();

            return roomsOfTypeDTO;
        }

        public IEnumerable<RoomDTO> GetRoomsWithHotels()
        {
            var roomsWithHotels = _roomRepository.GetRoomsWithHotels();
            var roomsWithHotelsDTO = roomsWithHotels.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                Number = room.Number,
                Hotel = new HotelDTO
                {
                    HotelId = room.Hotel.HotelId,
                    Name = room.Hotel.Name,
                    Rating = room.Hotel.Rating,
                    Address = room.Hotel.Address
                }
            }).ToList();

            return roomsWithHotelsDTO;
        }

        public IEnumerable<RoomDTO> GetRoomsWithReservations()
        {
            var roomsWithReservations = _roomRepository.GetRoomsWithReservations();

            var roomsWithReservationsDTO = roomsWithReservations.Select(room => new RoomDTO
            {
                RoomId = room.RoomId,
                Number = room.Number,
            }).ToList();

            return roomsWithReservationsDTO;
        }
    }
}
