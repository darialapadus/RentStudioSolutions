using RentStudio.Models.DTOs;
using RentStudio.Repositories.RoomTypeRepository;

namespace RentStudio.Services.RoomTypeService
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public bool Save()
        {
            return _roomTypeRepository.Save();
        }

        public IEnumerable<RoomTypeDTO> GetRoomTypes(FilterRoomTypeDTO filterRoomTypeDTO)
        {
            var anyRoomTypeFilter = AnyRoomTypeFilter(filterRoomTypeDTO);
            var roomTypes = anyRoomTypeFilter ? _roomTypeRepository.GetRoomTypes(filterRoomTypeDTO) : _roomTypeRepository.GetRoomTypes();
            return roomTypes.Select(t => new RoomTypeDTO
            {
                RoomTypeId = t.RoomTypeId,
                Name = t.Name,
                Facilities = t.Facilities,
                Price = t.Price

            }).ToList();
        }

        public void AddRoomTypes(RoomTypeDTO roomTypeDto)
        {
            _roomTypeRepository.AddRoomTypes(roomTypeDto);
        }

        public void UpdateRoomType(int id, RoomTypeShortDTO updatedRoomType)
        {
            _roomTypeRepository.UpdateRoomType(id, updatedRoomType);
        }

        public void DeleteRoomType(int id)
        {
            _roomTypeRepository.DeleteRoomType(id);
        }

        public IEnumerable<RoomTypeDTO> GetRoomTypesGroupedByName()
        {
            var roomTypesGroupedByName = _roomTypeRepository.GetRoomTypesGroupedByName();

            var roomTypesGroupedByNameDTO = roomTypesGroupedByName.Select(roomType => new RoomTypeDTO
            {
                RoomTypeId = roomType.RoomTypeId,
                Name = roomType.Name,
                Facilities = roomType.Facilities,
                Price = roomType.Price
            }).ToList();

            return roomTypesGroupedByNameDTO;
        }

        public IEnumerable<RoomTypeDTO> GetRoomTypesWithFacilities(string facilities)
        {
            var roomTypesWithFacilities = _roomTypeRepository.GetRoomTypesWithFacilities(facilities);
            return roomTypesWithFacilities.Select(t => new RoomTypeDTO
            {
                RoomTypeId = t.RoomTypeId,
                Name = t.Name,
                Facilities = t.Facilities,
                Price = t.Price
            }).ToList();
        }
        public IEnumerable<RoomTypeWithRoomsDTO> GetRoomTypesWithRooms()
        {
            return _roomTypeRepository.GetRoomTypesWithRooms();
        }

        private bool AnyRoomTypeFilter(FilterRoomTypeDTO filterRoomTypeDTO)
        {
            if (filterRoomTypeDTO == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(filterRoomTypeDTO.Name) ||
                !string.IsNullOrEmpty(filterRoomTypeDTO.Facilities) ||
                 filterRoomTypeDTO.Price != 0)
            {
                return true;
            }
            return false;
        }   
    }
}
