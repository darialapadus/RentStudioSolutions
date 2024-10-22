using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models.DTOs;
using RentStudio.Models.Enums;
using RentStudio.Repositories.PaymentRepository;
using RentStudio.Repositories.ReservationRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RentStudio.Controllers
{
    public class LinqController : BaseController
    {
        private readonly RentDbContext _context;

        public LinqController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet("linqPlay/{id}")]
        public async Task<ActionResult<string>> LinqPlay(int id)
        {
            //sa returnez toate platile cu valoarea mai mare de 150 si cu statusul succesed
            var paymentsSucceeded = await _context.Payments.Where(x => x.Amount > 150 && x.Status == PaymentStatus.Succeeded.ToString()).ToListAsync();
            //sa returnez suma totala a platilor cu id-ul 5 (acasa id uri multiple 1,3,5)
            var totalPayments = await _context.Payments.Where(x => x.PaymentId == 5).SumAsync(x => x.Amount);
            //sau
            var totalPayments1 = await _context.Payments.Select(x => new { x.Amount, x.PaymentId }).Where(x => x.PaymentId == 5).ToListAsync();
            var sumAmount = totalPayments1.Sum(x => x.Amount);
            //sa returnez cea mai mare plata, cu mentiunea ca vreau sa returnez doar coloana amount si coloana paymentId 
            var maxPayment = await _context.Payments.Select(x => new { x.Amount, x.PaymentId }).OrderByDescending(x => x.Amount).FirstOrDefaultAsync();
            //sau
            var maxPayment1 = await _context.Payments.Select(x => new { x.Amount, x.PaymentId }).MaxAsync(x => x.Amount);
            //sa returnez nr total de plati cu statusul pending
            var totalPayments2 = await _context.Payments.CountAsync(x => x.Status == PaymentStatus.Pending.ToString());
            //sau
            var totalPayments3 = await _context.Payments.Where(x => x.Status == PaymentStatus.Pending.ToString()).ToListAsync();
            var total = totalPayments3.Count();
            //sa returnez numarul total de rezervari cu conditia ca numarul de camere sa fie >2
            var totalReservations = await _context.Reservations.CountAsync(x => x.NumberOfRooms > 2);
            //sa returnez prima rezervare cu check in ul in data de azi
            var firstReservations = await _context.Reservations.FirstOrDefaultAsync(x => x.CheckInDate.Date == DateTime.UtcNow.Date);
            //sa returnez toate rezervarile cu check in ul mai mare de ziua de azi si check out ul peste 3 zile
            var futureReservations = await _context.Reservations.Where(x => x.CheckInDate > DateTime.UtcNow.Date && x.CheckOutDate > DateTime.UtcNow.AddDays(3)).ToListAsync();
            //sa returnez toate rezervarile cu numarul de guests >10, numarul camerelor>5 si customerid=3
            var filteredReservations = await _context.Reservations.Where(x => x.NumberOfGuests > 10 && x.NumberOfRooms > 5 && x.CustomerId == 3).ToListAsync();

            //sa returnez primele 3 rezervari cu numarul de camere mai mare de 3 din data de azi,
            //si cu raspunsul daca exista sa verificam platile Succeded pe aceste rezervari -de afisat lista cu platile cu succeded
            var threeReservations = await _context.Reservations
                                                    .Where(x => x.NumberOfRooms > 3 && x.CheckInDate.Date == DateTime.UtcNow.Date)
                                                    .Take(3)
                                                    .ToListAsync();
            var succededPayments = await _context.Payments
                                                    .Where(x => x.Status == PaymentStatus.Succeeded.ToString() && threeReservations
                                                    .Select(x => x.ReservationId).Contains(x.ReservationId))
                                                    .ToListAsync();
            //sa returnez ce mai mare plata cu conditia ca PaymentId sa aibe una din valorile 1,7,10,12
            var paymentIds = new List<int> { 1, 7, 10, 12 };
            var highPayment = await _context.Payments.Where(x => paymentIds.Contains(x.PaymentId)).OrderByDescending(x => x.Amount).FirstOrDefaultAsync();
            //sa returnez o lista de rezervari cu check in ul de astazi, numarul de camere=1 si care nu are nici o plata Succeded facuta 
            var reservationsList = await _context.Reservations.Where(x => x.CheckInDate.Date == DateTime.UtcNow.Date && x.NumberOfRooms == 1).ToListAsync();
            var paymentsList = await _context.Payments.Where(x => reservationsList.Select(x => x.ReservationId).Contains(x.ReservationId) && x.Status == PaymentStatus.Succeeded.ToString()).ToListAsync();
           
            /*var clientId = 7;
            var query = _context.Reservations.Where(x => x.CheckInDate > DateTime.UtcNow.AddDays(-7)).AsQueryable();
            if (clientId == 7)
            {
                query.Where(_ => query.Any(x => x.ReservationId == clientId));
            }
            var demo = query.ToQueryString();
            var reservations = await query.ToListAsync();*/

            //sa se afiseze clientId care a facut cele mai multe rezervari in ultima saptamana
            var query = await _context.Reservations.Where(x => x.CheckInDate > DateTime.UtcNow.AddDays(-7))
                                                   .GroupBy(x => x.CustomerId).Select(x => new
                                                   {
                                                       CustomerId = x.Key,
                                                       ReservationsCount = x.Count()
                                                   }).OrderByDescending(x=>x.ReservationsCount).FirstOrDefaultAsync();

            return Ok();
        }
    }

}
