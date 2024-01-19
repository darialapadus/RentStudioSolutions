using RentStudio.Models;

namespace RentStudio.Services
{
    public interface IHotelService
    {
        IEnumerable<HotelDTO> GetHotels();
        void AddHotel(HotelDTO hotelDto);
        void UpdateHotel(int id, HotelShortDTO updatedHotel);
        void DeleteHotel(int id);
        IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating();
        IEnumerable<HotelDTO> GetHotelsWithAddress(string address);
        IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms();

    }
}
