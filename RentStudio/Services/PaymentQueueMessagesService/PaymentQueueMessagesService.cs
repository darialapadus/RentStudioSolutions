using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Helpers.Middleware;
using RentStudio.Repositories.PaymentQueueMessagesRepository;
using RentStudio.Repositories.PaymentRepository;
using RentStudio.Repositories.ReservationRepository;
using RentStudio.Services.UserService;

namespace RentStudio.Services.PaymentQueueMessagesService
{
    public class PaymentQueueMessagesService : IPaymentQueueMessagesService
    {
        private readonly IPaymentQueueMessagesRepository _paymentQueueMessagesRepository;
     
        public PaymentQueueMessagesService(IPaymentQueueMessagesRepository paymentQueueMessagesRepository)
        {
            _paymentQueueMessagesRepository = paymentQueueMessagesRepository;
 
        }
        public async Task AddAsync(PaymentsQueueMessage message)
        {
            await _paymentQueueMessagesRepository.AddAsync(message);
        }

        public async Task<IEnumerable<PaymentsQueueMessage>> GetQueueMessagesAsync(int batchSize)
        {
            return await _paymentQueueMessagesRepository.GetQueueMessagesAsync(batchSize);
        }
        public async Task<IEnumerable<PaymentsQueueMessage>> GetQueueUnprocessedMessagesAsync(int batchSize)
        {
            return await _paymentQueueMessagesRepository.GetQueueUnprocessedMessagesAsync(batchSize);
        }
    }
}
