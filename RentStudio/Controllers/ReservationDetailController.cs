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

    }
}
