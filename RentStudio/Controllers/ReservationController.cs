using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;
using RentStudio.Services;

namespace RentStudio.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            var reservations = _reservationService.GetReservations();
            return Ok(reservations);
        }

        [HttpPost]
        public IActionResult AddReservation([FromBody] ReservationDTO reservation)
        {
            _reservationService.AddReservation(reservation);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] ReservationShortDTO updatedReservation)
        {
            _reservationService.UpdateReservation(id, updatedReservation);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            _reservationService.DeleteReservation(id);
            return Ok();
        }

        //GROUPBY pentru a grupa rezervarile in functie de status.
        [HttpGet("reservations/grouped-by-status")]
        public IActionResult GetReservationsGroupedByStatus()
        {
           var reservationsGroupedByStatus = _reservationService.GetGroupedReservationsByStatus();
            return Ok(reservationsGroupedByStatus);
        }

        //WHERE pentru a obtine toate rezervarile care au fost confirmate.
        [HttpGet("confirmed-reservations")]
        public IActionResult GetConfirmedReservations()
        {
            var confirmedReservations = _reservationService.GetConfirmedReservations();
            return Ok(confirmedReservations);
        }

        //INCLUDE pentru a incarca toate detaliile rezervarilor impreuna cu informatiile despre rezervari.
        [HttpGet("reservations-with-details")]
        public IActionResult GetReservationsWithDetails()
        {
            var reservationsWithDetails = _reservationService.GetReservationsWithDetails();
            return Ok(reservationsWithDetails);
        }

    }
}

