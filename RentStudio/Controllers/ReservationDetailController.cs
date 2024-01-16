using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Controllers
{
    public class ReservationDetailController : BaseController
    {
        private readonly RentDbContext _context;

        public ReservationDetailController(RentDbContext context)
        {
            _context = context;
        }

        [HttpGet] //folosit pt requesturi de tip READ-citim/luam date din baza
        public IActionResult GetReservationDetails()
        {
            var reservationDetails = _context.ReservationDetails.ToList();
            return Ok(reservationDetails);
        }

        [HttpPost] //folosit pt requesturi de tip WRITE-scriem date in baza(inserez o linie noua in tabela reservationDetails);HttpReq e format din Header si Body
        public IActionResult AddReservationDetails([FromBody] ReservationDetailDTO reservationDetail)
        {
            var entity = new ReservationDetail();
            entity.SpecialRequests = reservationDetail.SpecialRequests;
            entity.LastModified = reservationDetail.LastModified;
            entity.BillingInformation = reservationDetail.BillingInformation;
            entity.ReservationId = reservationDetail.ReservationId;
            _context.ReservationDetails.Add(entity);
            _context.SaveChanges();
            return Ok(reservationDetail);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservationDetail(int id, [FromBody] ReservationDetailShortDTO updatedReservationDetail)
        {
            var existingReservationDetail = _context.ReservationDetails.Find(id);
            if (existingReservationDetail == null)
                return NotFound();

            existingReservationDetail.SpecialRequests = updatedReservationDetail.SpecialRequests;

            _context.SaveChanges();
            return Ok(existingReservationDetail);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservationDetail(int id)
        {
            var reservationDetail = _context.ReservationDetails.Find(id);
            if (reservationDetail == null)
                return NotFound();

            _context.ReservationDetails.Remove(reservationDetail);
            _context.SaveChanges();
            return Ok(reservationDetail);
        }
    }
}
