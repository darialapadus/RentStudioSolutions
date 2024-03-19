using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.RoomTypeRepository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly RentDbContext _context;

        public RoomTypeRepository(RentDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<RoomType> GetRoomTypes()
        {
            return _context.RoomTypes.ToList();
        }

        public IEnumerable<RoomType> GetRoomTypes(FilterRoomTypeDTO filterRoomTypeDTO)
        {
            var query = _context.RoomTypes.AsQueryable();

            if (!string.IsNullOrEmpty(filterRoomTypeDTO.Name))
            {
                query = query.Where(x => x.Name == filterRoomTypeDTO.Name);
            }
            if (filterRoomTypeDTO.Price != 0)
            {
                query = query.Where(x => x.Price == filterRoomTypeDTO.Price);
            }
            if (!string.IsNullOrEmpty(filterRoomTypeDTO.Facilities))
            {
                query = query.Where(x => x.Name == filterRoomTypeDTO.Facilities);
            }

            return query.ToList();
        }
        public void AddRoomTypes(RoomTypeDTO roomTypeDto)
        {
            var entity = new RoomType
            {
                Name = roomTypeDto.Name,
                Facilities = roomTypeDto.Facilities,
                Price = roomTypeDto.Price
            };

            _context.RoomTypes.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateRoomType(int id, RoomTypeShortDTO updatedRoomType)
        {
            var existingRoomType = _context.RoomTypes.Find(id);
            if (existingRoomType == null)
            {
                throw new KeyNotFoundException("RoomType not found");
            }

            existingRoomType.Facilities = updatedRoomType.Facilities;
            existingRoomType.Price = updatedRoomType.Price;

            _context.SaveChanges();
        }

        public void DeleteRoomType(int id)
        {
            var roomType = _context.RoomTypes.Find(id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();
            }
        }

        public IEnumerable<RoomType> GetRoomTypesGroupedByName()
        {
            return _context.RoomTypes
                .GroupBy(rt => rt.Name)
                .Select(group => group.First()) 
                .ToList();
        }

        public IEnumerable<RoomType> GetRoomTypesWithFacilities(string facilities)
        {
            return _context.RoomTypes
                .Where(rt => rt.Facilities.Contains(facilities))
                .ToList();
        }

        public IEnumerable<RoomTypeWithRoomsDTO> GetRoomTypesWithRooms()
        {
            var roomTypesWithRooms = _context.RoomTypes
                .GroupJoin(
                    _context.Rooms,
                    roomType => roomType.RoomTypeId,
                    room => room.RoomTypeId,
                    (roomType, rooms) => new RoomTypeWithRoomsDTO
                    {
                        RoomTypeId = roomType.RoomTypeId,
                        Name = roomType.Name,
                        Facilities = roomType.Facilities,
                        Price = roomType.Price,
                        Rooms = rooms.Select(room => new RoomDTO
                        {
                            RoomId = room.RoomId,
                            Number = room.Number,
                            HotelId = room.HotelId
                        }).ToList()
                    })
                .ToList();

            return roomTypesWithRooms;
        }

    }
}
