using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly RentDbContext _context;

        public ReservationController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetReservations()
        {
            var reservations = _context.Reservations.ToList();
            return Ok(reservations);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela reservations);HttpReq e format din Header si Body
        public IActionResult AddReservations([FromBody] ReservationDTO reservation)
        {
            var entity = new Reservation();
            entity.CheckInDate = reservation.CheckInDate;
            entity.CheckOutDate = reservation.CheckOutDate;
            entity.NumberOfRooms = reservation.NumberOfRooms;
            entity.NumberOfGuests = reservation.NumberOfGuests;
            entity.Status = reservation.Status;
            entity.PaymentMethod = reservation.PaymentMethod;
            entity.CustomerId = reservation.CustomerId;
            _context.Reservations.Add(entity);
            _context.SaveChanges();
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] ReservationShortDTO updatedReservation)
        {
            var existingReservation = _context.Reservations.Find(id);
            if (existingReservation == null)
                return NotFound();

            existingReservation.CheckInDate = updatedReservation.CheckInDate;
            existingReservation.CheckOutDate = updatedReservation.CheckOutDate;
            existingReservation.NumberOfRooms = updatedReservation.NumberOfRooms;
            existingReservation.NumberOfGuests = updatedReservation.NumberOfGuests;

            _context.SaveChanges();
            return Ok(existingReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
                return NotFound();

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return Ok(reservation);
        }

        //GROUPBY pentru a grupa rezervarile in functie de status.
        [HttpGet("reservations/grouped-by-status")]
        public IActionResult GetReservationsGroupedByStatus()
        {
            var reservationsGroupedByStatus = _context.Reservations
                .GroupBy(r => r.Status)
                .Select(group => new
                {
                    Status = group.Key,
                    Reservations = group.ToList()
                })
                .ToList();

            return Ok(reservationsGroupedByStatus);
        }

        //WHERE pentru a obtine toate rezervarile care au fost confirmate.
        [HttpGet("confirmed-reservations")]
        public IActionResult GetConfirmedReservations()
        {
            var confirmedReservations = _context.Reservations
                .Where(r => r.Status == "Confirmed")
                .ToList();

            return Ok(confirmedReservations);
        }

        //JOIN intre Reservations si Customers pentru a obtine informatiile despre rezervari impreuna cu datele despre clienti.
        [HttpGet("reservations-with-customers")]
        public IActionResult GetReservationsWithCustomers()
        {
            var reservationsWithCustomers = _context.Reservations
                .Join(
                    _context.Customers,
                    reservation => reservation.CustomerId,
                    customer => customer.CustomerId,
                    (reservation, customer) => new
                    {
                        Reservation = new ReservationDTO
                        {
                            ReservationId = reservation.ReservationId,
                            CheckInDate = reservation.CheckInDate,
                            CheckOutDate = reservation.CheckOutDate,
                            NumberOfRooms = reservation.NumberOfRooms,
                            NumberOfGuests = reservation.NumberOfGuests,
                            Status = reservation.Status,
                            PaymentMethod = reservation.PaymentMethod,
                            CustomerId = reservation.CustomerId
                        },
                        Customer = new CustomerDTO
                        {
                            CustomerId = customer.CustomerId,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            City = customer.City
                        }
                    })
                .ToList();

            return Ok(reservationsWithCustomers);
        }


        //INCLUDE pentru a incarca toate detaliile rezervarilor impreuna cu informatiile despre rezervari.
        [HttpGet("reservations-with-details")]
        public IActionResult GetReservationsWithDetails()
        {
            var reservationsWithDetails = _context.Reservations
                .Include(r => r.ReservationDetail)
                .Select(reservation => new
                {
                    Reservation = new ReservationDTO
                    {
                        ReservationId = reservation.ReservationId,
                        CheckInDate = reservation.CheckInDate,
                        CheckOutDate = reservation.CheckOutDate,
                        NumberOfRooms = reservation.NumberOfRooms,
                        NumberOfGuests = reservation.NumberOfGuests,
                        Status = reservation.Status,
                        PaymentMethod = reservation.PaymentMethod,
                        CustomerId = reservation.CustomerId
                    },
                    ReservationDetail = new ReservationDetailDTO
                    {
                        ReservationId = reservation.ReservationDetail.ReservationId,
                        SpecialRequests = reservation.ReservationDetail.SpecialRequests,
                        LastModified = reservation.ReservationDetail.LastModified,
                        BillingInformation = reservation.ReservationDetail.BillingInformation
                    }
                })
                .ToList();

            return Ok(reservationsWithDetails);
        }


    }
}
