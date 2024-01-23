using RentStudio.Models;
using RentStudio.Repositories;

namespace RentStudio.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public IEnumerable<HotelDTO> GetHotels()
        {
            var hotels = _hotelRepository.GetHotels();
            return hotels.Select(h => new HotelDTO
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Rating = h.Rating, 
                Address = h.Address, 
            }).ToList();
        }

        public void AddHotel(HotelDTO hotelDto)
        {
            _hotelRepository.AddHotel(hotelDto);
        }

        void IHotelService.UpdateHotel(int id, HotelShortDTO updatedHotel)
        {
            _hotelRepository.UpdateHotel(id, updatedHotel);
        }

        public void DeleteHotel(int id)
        {
            _hotelRepository.DeleteHotel(id);
        }
        
        public IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating()
        {
            return _hotelRepository.GetHotelsGroupedByRating();
        }

        public IEnumerable<HotelDTO> GetHotelsWithAddress(string address)
        {
            return _hotelRepository.GetHotelsWithAddress(address);
        }

        public IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms()
        {
            return _hotelRepository.GetHotelsWithRooms();
        }

    }
}
