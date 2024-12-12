using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;

namespace RentStudio.Repositories.PaymentQueueMessagesRepository
{
    public class PaymentQueueMessagesRepository : IPaymentQueueMessagesRepository
    {
        private readonly RentDbContext _context;

        public PaymentQueueMessagesRepository(RentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentsQueueMessage>> GetQueueMessagesAsync(int batchSize)
        {
            return await _context.PaymentsQueueMessages.Take(batchSize).ToListAsync();
        }

        public async Task AddAsync(PaymentsQueueMessage message)
        {
            await _context.PaymentsQueueMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
    }
}
