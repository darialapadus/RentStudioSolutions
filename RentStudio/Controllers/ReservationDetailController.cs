using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;
using RentStudio.Services;

namespace RentStudio.Controllers
{
    public class ReservationDetailController : BaseController
    {
        private readonly IReservationDetailService _reservationDetailService;

        public ReservationDetailController(IReservationDetailService reservationDetailService)
        {
            _reservationDetailService = reservationDetailService;
        }

        [HttpGet]
        public IActionResult GetReservationDetails()
        {
            var reservationDetails = _reservationDetailService.GetReservationDetails();
            return Ok(reservationDetails);
        }

        [HttpPost]
        public IActionResult AddReservationDetail([FromBody] ReservationDetailDTO reservationDetail)
        {
            _reservationDetailService.AddReservationDetail(reservationDetail);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservationDetail(int id, [FromBody] ReservationDetailShortDTO updatedReservationDetail)
        {
            _reservationDetailService.UpdateReservationDetail(id, updatedReservationDetail);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservationDetail(int id)
        {
            _reservationDetailService.DeleteReservation(id);
            return Ok();
        }

        // GROUPBY pentru a grupa detaliile rezervarilor in functie de solicitarile speciale.
        [HttpGet("reservationdetails")]
        public IActionResult GetReservationDetailsGroupedByRequests()
        {
            var reservationDetails = _reservationDetailService.GetReservationDetails();
            return Ok(reservationDetails);
        }

        // WHERE pentru a obtine toate detaliile rezervarilor care au fost modificate recent.
        [HttpGet("modified-reservationdetails")]
        public IActionResult GetModifiedReservationDetails()
        {
            var modifiedReservationDetails = _reservationDetailService.GetModifiedReservationDetails();

            return Ok(modifiedReservationDetails);
        }

        // JOIN intre ReservationDetails si Reservations pentru a obtine informatiile despre detaliile rezervarilor impreuna cu datele despre rezervari.
        [HttpGet("reservationdetails/grouped-by-requests")]
        public IActionResult GetReservationDetailsWithReservations()
        {
            var reservationDetailsGroupedByRequests = _reservationDetailService.GetReservationDetailsGroupedByRequests(); // Aici se utilizează metoda din serviciu care returnează ReservationDetailGroupedByRequestsDTO

            return Ok(reservationDetailsGroupedByRequests);
        }
    }
}
