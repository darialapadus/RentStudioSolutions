using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.RoomTypeRepository
{
    public interface IRoomTypeRepository
    {
        IEnumerable<RoomType> GetRoomTypes();
        void AddRoomTypes(RoomTypeDTO roomType);
        void UpdateRoomType(int id, RoomTypeShortDTO updatedRoomType);
        void DeleteRoomType(int id);
        bool Save();
        IEnumerable<RoomType> GetRoomTypesGroupedByName();
        IEnumerable<RoomType> GetRoomTypesWithFacilities(string facilities);
        IEnumerable<RoomTypeWithRoomsDTO> GetRoomTypesWithRooms();
    }
}
