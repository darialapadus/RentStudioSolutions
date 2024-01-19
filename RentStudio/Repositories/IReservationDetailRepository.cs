using RentStudio.Models;

namespace RentStudio.Repositories
{
    public interface IReservationDetailRepository
    {
        IEnumerable<ReservationDetailDTO> GetReservationDetails();
        void AddReservationDetail(ReservationDetailDTO reservationDetail);
        void UpdateReservationDetail(int id, ReservationDetailShortDTO updatedReservationDetail);
        void DeleteReservationDetail(int id);

    }
}
