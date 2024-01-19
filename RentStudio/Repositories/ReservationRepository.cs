using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RentDbContext _context;

        public ReservationRepository(RentDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReservationDTO> GetReservations()
        {
            return _context.Reservations
                .Select(r => new ReservationDTO
                {
                    ReservationId = r.ReservationId,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfRooms = r.NumberOfRooms,
                    NumberOfGuests = r.NumberOfGuests,
                    Status = r.Status,
                    PaymentMethod = r.PaymentMethod,
                    CustomerId = r.CustomerId
                })
                .ToList();
        }

        public void AddReservation(ReservationDTO reservation)
        {
            var entity = new Reservation
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                NumberOfRooms = reservation.NumberOfRooms,
                NumberOfGuests = reservation.NumberOfGuests,
                Status = reservation.Status,
                PaymentMethod = reservation.PaymentMethod,
                CustomerId = reservation.CustomerId
            };
            _context.Reservations.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateReservation(int id, ReservationShortDTO updatedReservation)
        {
            var existingReservation = _context.Reservations.Find(id);
            if (existingReservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }

            existingReservation.CheckInDate = updatedReservation.CheckInDate;
            existingReservation.CheckOutDate = updatedReservation.CheckOutDate;
            existingReservation.NumberOfRooms = updatedReservation.NumberOfRooms;
            existingReservation.NumberOfGuests = updatedReservation.NumberOfGuests;

            _context.SaveChanges();
        }

        public void DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<GroupedReservationsByStatusDTO> GetReservationsGroupedByStatus()
        {
            var groupedReservations = _context.Reservations
                .GroupBy(r => r.Status)
                .Select(group => new GroupedReservationsByStatusDTO
                {
                    Status = group.Key,
                    Reservations = group.Select(r => new ReservationDTO
                    {
                        ReservationId = r.ReservationId,
                        CheckInDate = r.CheckInDate,
                        CheckOutDate = r.CheckOutDate,
                        NumberOfRooms = r.NumberOfRooms,
                        NumberOfGuests = r.NumberOfGuests,
                        Status = r.Status,
                        PaymentMethod = r.PaymentMethod,
                        CustomerId = r.CustomerId
                    }).ToList()
                })
                .ToList();

            return groupedReservations;
        }

        public IEnumerable<ReservationDTO> GetConfirmedReservations()
        {
            var confirmedReservations = _context.Reservations
                .Where(r => r.Status == "Confirmed")
                .ToList();

            var confirmedReservationsDTO = confirmedReservations.Select(r => new ReservationDTO
            {
                ReservationId = r.ReservationId,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                NumberOfRooms = r.NumberOfRooms,
                NumberOfGuests = r.NumberOfGuests,
                Status = r.Status,
                PaymentMethod = r.PaymentMethod,
                CustomerId = r.CustomerId
            }).ToList();

            return confirmedReservationsDTO;
        }

        public IEnumerable<ReservationDTO> GetReservationsWithDetails()
        {
            var reservationsWithDetails = _context.Reservations
                .Include(r => r.ReservationDetail)
                .Select(reservation => new ReservationDTO
                {
                    ReservationId = reservation.ReservationId,
                    CheckInDate = reservation.CheckInDate,
                    CheckOutDate = reservation.CheckOutDate,
                    NumberOfRooms = reservation.NumberOfRooms,
                    NumberOfGuests = reservation.NumberOfGuests,
                    Status = reservation.Status,
                    PaymentMethod = reservation.PaymentMethod,
                    CustomerId = reservation.CustomerId,
                    ReservationDetail = new ReservationDetailDTO
                    {
                        SpecialRequests = reservation.ReservationDetail.SpecialRequests,
                        LastModified = reservation.ReservationDetail.LastModified,
                        BillingInformation = reservation.ReservationDetail.BillingInformation
                    }
                })
                .ToList();

            return reservationsWithDetails;
        }
    }
}
