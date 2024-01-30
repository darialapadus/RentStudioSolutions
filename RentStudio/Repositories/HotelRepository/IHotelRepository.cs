using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.HotelRepository
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetHotels();
        void AddHotel(HotelDTO hotelDto);
        void UpdateHotel(int id, HotelShortDTO updatedHotel);
        void DeleteHotel(int id);
        bool Save();
        IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating();
        IEnumerable<HotelDTO> GetHotelsWithAddress(string address);
        IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms();
    }
}
