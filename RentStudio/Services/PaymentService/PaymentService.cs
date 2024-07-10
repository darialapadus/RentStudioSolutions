using RentStudio.DataAccesLayer;
using RentStudio.Helpers;
using RentStudio.Models.DTOs;
using RentStudio.Repositories.PaymentRepository;
using RentStudio.Repositories.UserRepository;
using RentStudio.Services.UserService;

namespace RentStudio.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserService _userService;
        public PaymentService(IPaymentRepository paymentRepository, IUserService userService)
        {
            _paymentRepository = paymentRepository;
            _userService = userService;

        }

        public async Task ProcessPaymentAsync(PaymentDTO paymentDTO)
        {
            var userId = _userService.PartialRegisterUser(paymentDTO);
            //_paymentProcessingService.ProcessPayment(paymentDTO, userId);
            var payment = new Payment
            {
                UserId = userId,
                ReservationId = paymentDTO.ReservationId,
                Amount = paymentDTO.Amount,
                Status = "Pending",
                TransactionDate = DateTime.UtcNow
            };
            await SavePaymentAsync(payment, true);

            var paymentValidated = new Payment
            {
                UserId = userId,
                ReservationId = paymentDTO.ReservationId,
                Amount = paymentDTO.Amount,
                Status = await ValidatePaymentAsync(paymentDTO),
                TransactionDate = DateTime.UtcNow
            };

            await SavePaymentAsync(paymentValidated, true);

            payment.Status = "Processed";

            await SavePaymentAsync(payment, false);


        }
        public async Task SavePaymentAsync(Payment payment, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    await _paymentRepository.AddAsync(payment);
                }
                await _paymentRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the payment.", ex);
            }
        }
        public async Task<string> ValidatePaymentAsync(PaymentDTO paymentDTO)
        {
            var status = await BankSimulator.BankProcessPaymentAsync();
            return status;
        }
        public async Task<string> CheckPaymentStatusAsync(Guid userId, int reservationId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByUserIdAsync(userId, reservationId);

                if (payment == null)
                {
                    return Constants.ErrorMessages.PaymentNotFound;
                }
                return payment.Status;
                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking payment status.", ex);
            }
        }
    }

}
