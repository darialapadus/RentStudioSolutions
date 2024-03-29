﻿using RentStudio.Models.DTOs;
using RentStudio.Repositories.HotelRepository;

namespace RentStudio.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public string GetHotelNameById(int hotelId)
        {
            return _hotelRepository.GetHotelNameById(hotelId);
        }
        public int GetNumberOfRooms(int hotelId)
        {
            return _hotelRepository.GetNumberOfRooms(hotelId);
        }
        public IEnumerable<HotelDTO> GetHotels(FilterHotelDTO filterHotelDTO)
        {
            var anyHotelFilter = AnyHotelFilter(filterHotelDTO);
            var hotels = anyHotelFilter ? _hotelRepository.GetHotels(filterHotelDTO) : _hotelRepository.GetHotels();
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

        private bool AnyHotelFilter(FilterHotelDTO filterHotelDTO)
        {
            if(filterHotelDTO == null)
                return false;

            if(!string.IsNullOrEmpty(filterHotelDTO.Address) || 
                filterHotelDTO.Rating !=0)
                return true;
            
            return false;
        }
    }
}
