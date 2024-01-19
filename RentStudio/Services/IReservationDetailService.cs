using RentStudio.Models;

namespace RentStudio.Services
{
    public interface IReservationDetailService
    {
        IEnumerable<ReservationDetailDTO> GetReservationDetails();
        void AddReservationDetail(ReservationDetailDTO reservationDetail);
        void UpdateReservationDetail(int id, ReservationDetailShortDTO updatedReservationDetail);
        void DeleteReservation(int id);
        IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsGroupedByRequests();
        IEnumerable<ReservationDetailDTO> GetModifiedReservationDetails();
        IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsWithReservations();

    }
}
