using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.HotelRepository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly RentDbContext _context;

        public HotelRepository(RentDbContext context)
        {
            _context = context;
        }

        bool IHotelRepository.Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels.ToList();
        }

        public void AddHotel(HotelDTO hotelDto)
        {
            var entity = new Hotel
            {
                Name = hotelDto.Name,
                Rating = hotelDto.Rating,
                Address = hotelDto.Address,
            };
            _context.Hotels.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateHotel(int id, HotelShortDTO updatedHotel)
        {
            var existingHotel = _context.Hotels.Find(id);
            if (existingHotel == null)
            {
                throw new KeyNotFoundException("Hotel not found");
            }

            existingHotel.Rating = updatedHotel.Rating;

            _context.SaveChanges();
        }

        public void DeleteHotel(int id)
        {
            var hotel = _context.Hotels.Find(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                _context.SaveChanges();
            }
        }

        public IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating()
        {
            return _context.Hotels
                .GroupBy(h => h.Rating)
                .Select(group => new GroupedHotelsByRatingDTO
                {
                    Rating = group.Key,
                    Hotels = group.Select(h => new HotelDTO
                    {
                        HotelId = h.HotelId,
                        Name = h.Name,
                        Rating = h.Rating,
                        Address = h.Address,
                    }).ToList()
                })
                .ToList();
        }

        public IEnumerable<HotelDTO> GetHotelsWithAddress(string address)
        {
            return _context.Hotels
                .Where(hotel => hotel.Address.Contains(address))
                .Select(hotel => new HotelDTO
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    Rating = hotel.Rating,
                })
                .ToList();
        }

        public IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms()
        {
            var hotelsWithRooms = _context.Hotels
        .Join(
            _context.Rooms,
            hotel => hotel.HotelId,
            room => room.HotelId,
            (hotel, room) => new HotelWithRoomsDTO
            {
                Hotel = new HotelDTO
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Rating = hotel.Rating,
                    Address = hotel.Address,
                },
                Rooms = new List<RoomDTO>
                {
                    new RoomDTO
                    {
                        RoomId = room.RoomId,
                        Number = room.Number,
                        RoomTypeId = room.RoomTypeId,
                        HotelId = room.HotelId,
                    }
                }
            })
        .ToList();

            return hotelsWithRooms;
        }

    }
}
