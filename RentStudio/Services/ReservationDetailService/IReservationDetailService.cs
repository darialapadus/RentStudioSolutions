using RentStudio.Models.DTOs;

namespace RentStudio.Services.ReservationDetailService
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
