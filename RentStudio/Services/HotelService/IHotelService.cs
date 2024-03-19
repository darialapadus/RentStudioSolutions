using RentStudio.Models.DTOs;

namespace RentStudio.Services.HotelService
{
    public interface IHotelService
    {
        IEnumerable<HotelDTO> GetHotels(FilterHotelDTO filterHotelDTO);
        void AddHotel(HotelDTO hotelDto);
        void UpdateHotel(int id, HotelShortDTO updatedHotel);
        void DeleteHotel(int id);
        IEnumerable<GroupedHotelsByRatingDTO> GetHotelsGroupedByRating();
        IEnumerable<HotelDTO> GetHotelsWithAddress(string address);
        IEnumerable<HotelWithRoomsDTO> GetHotelsWithRooms();
    }
}
