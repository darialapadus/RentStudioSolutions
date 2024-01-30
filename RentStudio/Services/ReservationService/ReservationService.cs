using RentStudio.Models.DTOs;
using RentStudio.Repositories.ReservationRepository;

namespace RentStudio.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<ReservationDTO> GetReservations()
        {
            return _reservationRepository.GetReservations();
        }

        public void AddReservation(ReservationDTO reservation)
        {
            _reservationRepository.AddReservation(reservation);
        }

        public void UpdateReservation(int id, ReservationShortDTO updatedReservation)
        {
            _reservationRepository.UpdateReservation(id, updatedReservation);
        }

        public void DeleteReservation(int id)
        {
            _reservationRepository.DeleteReservation(id);
        }

        public IEnumerable<GroupedReservationsByStatusDTO> GetGroupedReservationsByStatus()
        {
            return _reservationRepository.GetReservationsGroupedByStatus();
        }

        public IEnumerable<ReservationDTO> GetConfirmedReservations()
        {
            return _reservationRepository.GetConfirmedReservations();
        }

        public IEnumerable<ReservationDTO> GetReservationsWithDetails()
        {
            return _reservationRepository.GetReservationsWithDetails();
        }

    }
}
