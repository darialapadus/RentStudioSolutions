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

    }
}
