using RentStudio.Models.DTOs;

namespace RentStudio.Services.ReservationService
{
    public interface IReservationService
    {
        IEnumerable<ReservationDTO> GetReservations();
        void AddReservation(ReservationDTO reservation);
        void UpdateReservation(int id, ReservationShortDTO updatedReservation);
        void DeleteReservation(int id);
        IEnumerable<GroupedReservationsByStatusDTO> GetGroupedReservationsByStatus();
        IEnumerable<ReservationDTO> GetConfirmedReservations();
        IEnumerable<ReservationDTO> GetReservationsWithDetails();
    }
}
