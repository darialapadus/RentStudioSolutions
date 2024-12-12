using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;

namespace RentStudio.Services.PaymentService
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(PaymentDTO paymentDTO);
        Task SavePaymentAsync(Payment payment, bool isNew);
        Task<string> CheckPaymentStatusAsync(Guid userId, int reservationId);
        List<PaymentDetailsDTO> GetPaymentsByUserId(Guid userId);
        Task<string> RefundPaymentAsync(Guid userId, int reservationId);
        Task<decimal>GetPaymentAmount(Guid userId, int reservationId);
    }
}
