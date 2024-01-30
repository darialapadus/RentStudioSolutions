using RentStudio.Models.DTOs;

namespace RentStudio.Repositories.ReservationRepository
{
    public interface IReservationRepository
    {
        IEnumerable<ReservationDTO> GetReservations();
        void AddReservation(ReservationDTO reservation);
        void UpdateReservation(int id, ReservationShortDTO updatedReservation);
        void DeleteReservation(int id);
        IEnumerable<GroupedReservationsByStatusDTO> GetReservationsGroupedByStatus();
        IEnumerable<ReservationDTO> GetConfirmedReservations();
        IEnumerable<ReservationDTO> GetReservationsWithDetails();
    }
}
