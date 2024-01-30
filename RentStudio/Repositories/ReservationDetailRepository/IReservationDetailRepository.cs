using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.ReservationDetailRepository
{
    public interface IReservationDetailRepository
    {
        IEnumerable<ReservationDetailDTO> GetReservationDetails();
        void AddReservationDetail(ReservationDetailDTO reservationDetail);
        void UpdateReservationDetail(int id, ReservationDetailShortDTO updatedReservationDetail);
        void DeleteReservationDetail(int id);
        IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsGroupedByRequests();
        IEnumerable<ReservationDetailDTO> GetModifiedReservationDetails();
        IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsWithReservations();
    }
}
