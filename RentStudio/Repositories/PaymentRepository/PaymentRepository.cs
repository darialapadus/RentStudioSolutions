using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.Enums;

namespace RentStudio.Repositories.PaymentRepository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RentDbContext _context;

        public PaymentRepository(RentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment); //adauga in obiectul/entitatea de Payments DTO-ul primit ca parametru, dar nu salveaza in baza de date pana la apelarea SaveChangesAsync
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(); //nu se salveaza nimic in baza pana nu se apeleaza SaveChangesAsync
        }
        public async Task<Payment> GetPaymentByUserIdAsync(Guid userId, int reservationId) //daca payment cu status processed exista cautam iar in baza dupa userid ultima valoare existenta
        {
            var payment = await _context.Payments.OrderBy(p => p.TransactionDate).LastOrDefaultAsync(p => p.UserId == userId && p.Status == PaymentStatus.Processed.ToString() && p.ReservationId == reservationId);
            var lastPayment = new Payment();
            if (payment != null)
            {   
                lastPayment = await _context.Payments
                    .OrderBy(p => p.TransactionDate)
                    .LastOrDefaultAsync(p => p.UserId == userId && p.ReservationId == reservationId); 
            }
            return lastPayment;
        }
        public List<Payment> GetPaymentsByUserId(Guid userId)
        {
            return _context.Payments
                .Include(p => p.User)   //echivalentul unui join in sql intre Payments si User avand foreign key-ul UserId
                .Include(p => p.Reservation)
                .Where(p => p.UserId == userId)
                .ToList();
        }
        public async Task<List<Payment>> GetPaymentsAsync(Guid userId, int reservationId)
        {
            var payment = await _context.Payments.Where(p => p.UserId == userId && p.ReservationId == reservationId).ToListAsync();
            return payment;

        }

        public async Task<List<Payment>> GetPaymentsByReservationIdAsync(int reservationId)
        {
            return await _context.Payments
                .Where(p => p.ReservationId == reservationId)
                .ToListAsync();
        }
    }
}
