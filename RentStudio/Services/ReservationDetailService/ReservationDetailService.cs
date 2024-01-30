using RentStudio.Repositories.ReservationDetailRepository;
using RentStudio.Models.DTOs;

namespace RentStudio.Services.ReservationDetailService
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailRepository _reservationDetailRepository;

        public ReservationDetailService(IReservationDetailRepository reservationDetailRepository)
        {
            _reservationDetailRepository = reservationDetailRepository;
        }

        public IEnumerable<ReservationDetailDTO> GetReservationDetails()
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

        public IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsGroupedByRequests()
        {
            return _reservationDetailRepository.GetReservationDetailsGroupedByRequests();
        }

        public IEnumerable<ReservationDetailDTO> GetModifiedReservationDetails()
        {
            var modifiedReservationDetails = _reservationDetailRepository.GetReservationDetails()
                .Where(rd => rd.LastModified != null)
                .ToList();

            return modifiedReservationDetails;
        }

        public IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsWithReservations()
        {
            return _reservationDetailRepository.GetReservationDetailsWithReservations();
        }

    }
}
