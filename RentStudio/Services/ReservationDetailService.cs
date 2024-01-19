using RentStudio.DataAccesLayer;
using RentStudio.Repositories;
using RentStudio.Models;

namespace RentStudio.Services
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailRepository _reservationDetailRepository;

        public ReservationDetailService(IReservationDetailRepository reservationDetailRepository) 
        {
            _reservationDetailRepository = reservationDetailRepository;
        }
        public IEnumerable<ReservationDetailDTO>GetReservationDetails()
        {
            return _reservationDetailRepository.GetReservationDetails();
        }
        public void AddReservationDetail(ReservationDetailDTO reservationDetail)
        {
            _reservationDetailRepository.AddReservationDetail(reservationDetail);
        }
        public void UpdateReservationDetail(int id, ReservationDetailShortDTO updatedReservationDetail)
        {
            _reservationDetailRepository.UpdateReservationDetail(id, updatedReservationDetail);
        }
        public void DeleteReservation(int reservationId)
        {
            _reservationDetailRepository.DeleteReservationDetail(reservationId);
        }

    }
}
