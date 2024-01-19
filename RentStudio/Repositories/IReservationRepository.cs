using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Repositories
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
