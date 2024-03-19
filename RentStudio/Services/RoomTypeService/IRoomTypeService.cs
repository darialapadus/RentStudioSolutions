using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Services.RoomTypeService
{
    public interface IRoomTypeService
    {
        IEnumerable<RoomTypeDTO> GetRoomTypes(FilterRoomTypeDTO filterRoomTypeDTO);
        void AddRoomTypes(RoomTypeDTO roomType);
        void UpdateRoomType(int id, RoomTypeShortDTO updatedRoomType);
        void DeleteRoomType(int id);
        bool Save();
        IEnumerable<RoomTypeDTO> GetRoomTypesGroupedByName();
        IEnumerable<RoomTypeDTO> GetRoomTypesWithFacilities(string facilities);
        IEnumerable<RoomTypeWithRoomsDTO> GetRoomTypesWithRooms();
    }
}
