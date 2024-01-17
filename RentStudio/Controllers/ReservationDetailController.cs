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

        // GROUPBY pentru a grupa detaliile rezervarilor in functie de solicitarile speciale.
        [HttpGet("reservationdetails/grouped-by-requests")]
        public IActionResult GetReservationDetailsGroupedByRequests()
        {
            var reservationDetailsGroupedByRequests = _context.ReservationDetails
                .GroupBy(rd => rd.SpecialRequests)
                .Select(group => new
                {
                    SpecialRequests = group.Key,
                    ReservationDetails = group.ToList()
                })
                .ToList();

            return Ok(reservationDetailsGroupedByRequests);
        }

        // WHERE pentru a obtine toate detaliile rezervarilor care au fost modificate recent.
        [HttpGet("modified-reservationdetails")]
        public IActionResult GetModifiedReservationDetails()
        {
            var modifiedReservationDetails = _context.ReservationDetails
                .Where(rd => rd.LastModified != null)
                .ToList();

            return Ok(modifiedReservationDetails);
        }

        // JOIN intre ReservationDetails si Reservations pentru a obtine informatiile despre detaliile rezervarilor impreuna cu datele despre rezervari.
        [HttpGet("reservationdetails-with-reservations")]
        public IActionResult GetReservationDetailsWithReservations()
        {
            var reservationDetailsWithReservations = _context.ReservationDetails
                .Join(
                    _context.Reservations,
                    reservationDetail => reservationDetail.ReservationId,
                    reservation => reservation.ReservationId,
                    (reservationDetail, reservation) => new
                    {
                        ReservationDetail = new ReservationDetailDTO
                        {
                            ReservationId = reservationDetail.ReservationId,
                            SpecialRequests = reservationDetail.SpecialRequests,
                            LastModified = reservationDetail.LastModified,
                            BillingInformation = reservationDetail.BillingInformation
                        },
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
                        }
                    })
                .ToList();

            return Ok(reservationDetailsWithReservations);
        }


    }
}
