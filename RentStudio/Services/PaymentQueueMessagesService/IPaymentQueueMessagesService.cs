using RentStudio.DataAccesLayer;

namespace RentStudio.Services.PaymentQueueMessagesService
{
    public interface IPaymentQueueMessagesService
    {
        Task<IEnumerable<PaymentsQueueMessage>> GetQueueMessagesAsync(int batchSize);
        
        Task<IEnumerable<PaymentsQueueMessage>> GetQueueUnprocessedMessagesAsync(int batchSize);

        Task AddAsync(PaymentsQueueMessage message);
    }
}
