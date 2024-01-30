using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Services.RoomService
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetRooms();
        void AddRoom(RoomDTO room);
        void UpdateRoom(int id, RoomShortDTO updatedRoom);
        void DeleteRoom(int id);
        bool Save();
        IEnumerable<RoomGroupedDTO> GetRoomsGroupedByNumber();
        IEnumerable<RoomDTO> GetRoomsOfType(int roomTypeId);
        IEnumerable<RoomDTO> GetRoomsWithHotels();
        IEnumerable<RoomDTO> GetRoomsWithReservations();
    }
}
