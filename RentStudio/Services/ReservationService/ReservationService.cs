using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Models.Enums;
using RentStudio.Repositories.PaymentRepository;
using RentStudio.Repositories.ReservationRepository;
using RentStudio.Services.UserService;

namespace RentStudio.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentRepository _paymentRepository;


        public ReservationService(IReservationRepository reservationRepository , IPaymentRepository paymentRepository)
        {
            _reservationRepository = reservationRepository;
            _paymentRepository = paymentRepository;
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
        public async Task<string> UpdateReservationAndPaymentsAsync(UpdateReservationDTO updateReservationDTO)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(updateReservationDTO.ReservationId);
            if (reservation == null)
            {
                return "Reservation not found!";
            }

            var payments = await _paymentRepository.GetPaymentsByReservationIdAsync(updateReservationDTO.ReservationId);
            var succeededPayment = payments.Where(p => p.Status == PaymentStatus.Succeeded.ToString()).ToList();

            reservation.NumberOfRooms = updateReservationDTO.NumberOfRooms;
            await _reservationRepository.UpdateAsync(reservation);
            await _reservationRepository.SaveAsync();

            if (succeededPayment != null)
            {
                decimal difference = updateReservationDTO.NewAmount - succeededPayment.Sum(x => x.Amount);

                if (difference > 0)
                {
                    var additionalPayment = new Payment
                    {
                        UserId = succeededPayment.Select(x => x.UserId).FirstOrDefault(),
                        ReservationId = reservation.ReservationId,
                        Amount = difference,
                        Status = PaymentStatus.Pending.ToString(),
                        TransactionDate = DateTime.UtcNow
                    };

                    await _paymentRepository.AddAsync(additionalPayment);
                    await _paymentRepository.SaveAsync();

                    return $"Reservation updated. Additional payment of {difference} is required."; //si apelez add payment
                }
                else if (difference < 0)
                {
                    var refundPayment = new Payment
                    {
                        UserId = succeededPayment.Select(x => x.UserId).FirstOrDefault(),
                        ReservationId = reservation.ReservationId,
                        Amount = Math.Abs(difference),
                        Status = PaymentStatus.Refund.ToString(),
                        TransactionDate = DateTime.UtcNow
                    };

                    await _paymentRepository.AddAsync(refundPayment);
                    await _paymentRepository.SaveAsync();

                    return $"Reservation updated. A refund of {Math.Abs(difference)} has been processed."; 
                }
                else
                {
                    return "Reservation updated with no change in payment amount.";
                }
            }
            else
            {
                return "Reservation updated without any existing payments.";
            }
        }

    }
}
