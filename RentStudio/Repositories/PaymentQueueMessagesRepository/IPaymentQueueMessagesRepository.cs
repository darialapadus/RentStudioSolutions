using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.PaymentQueueMessagesRepository
{
    public interface IPaymentQueueMessagesRepository
    {
        Task<IEnumerable<PaymentsQueueMessage>> GetQueueMessagesAsync(int batchSize);
        Task AddAsync(PaymentsQueueMessage message);
    }
}
