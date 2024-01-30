using RentStudio.Models.DTOs;
using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.RoomRepository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        void AddRoom(RoomDTO room);
        void UpdateRoom(int id, RoomShortDTO updatedRoom);
        void DeleteRoom(int id);
        bool Save();
        IEnumerable<RoomGroupedDTO> GetRoomsGroupedByNumber();
        IEnumerable<Room> GetRoomsOfType(int roomTypeId);
        IEnumerable<Room> GetRoomsWithHotels();
        IEnumerable<Room> GetRoomsWithReservations();
    }
}
