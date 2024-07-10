using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.PaymentRepository
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task SaveAsync();
        Task<Payment> GetPaymentByUserIdAsync(Guid userId, int reservationId);

    }
}
