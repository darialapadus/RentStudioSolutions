using RentStudio.Models;

namespace RentStudio.Services
{
    public interface IHotelService
    {
        void AddHotel(HotelDTO hotelDto);
        IEnumerable<HotelDTO> GetHotels();
        void DeleteHotel(int id);
        void UpdateHotel(int id, HotelShortDTO updatedHotel);
        IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating();
        IEnumerable<HotelDTO> GetHotelsWithAddress(string address);
        IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms();

    }
}
