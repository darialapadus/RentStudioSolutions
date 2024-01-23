using Microsoft.EntityFrameworkCore;
using RentStudio.DataAccesLayer;
using RentStudio.Models;

namespace RentStudio.Repositories
{
    public class ReservationDetailRepository : IReservationDetailRepository
    {
        private readonly RentDbContext _context;

        public ReservationDetailRepository(RentDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReservationDetailDTO> GetReservationDetails()
        {
            var reservationDetails = _context.ReservationDetails.ToList();
            var reservationDetailDTOs = reservationDetails.Select(rd => new ReservationDetailDTO
            {
                ReservationId = rd.ReservationId,
                SpecialRequests = rd.SpecialRequests,
                LastModified = rd.LastModified,
                BillingInformation = rd.BillingInformation
            }).ToList();

            return reservationDetailDTOs;
        }

        public void AddReservationDetail(ReservationDetailDTO reservationDetailDTO)
        {
            var entity = new ReservationDetail
            {
                SpecialRequests = reservationDetailDTO.SpecialRequests,
                LastModified = reservationDetailDTO.LastModified,
                BillingInformation = reservationDetailDTO.BillingInformation,
                ReservationId = reservationDetailDTO.ReservationId
            };
            _context.ReservationDetails.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateReservationDetail(int id, ReservationDetailShortDTO updatedReservationDetail)
        {
            var existingReservationDetail = _context.ReservationDetails.Find(id);
            if (existingReservationDetail == null)
            {
                throw new KeyNotFoundException("ReservationDetail not found");
            }

            existingReservationDetail.SpecialRequests = updatedReservationDetail.SpecialRequests;

            _context.SaveChanges();
        }

        public void DeleteReservationDetail(int id)
        {
            var reservationDetail = _context.ReservationDetails.Find(id);
            if (reservationDetail != null)
            {
                _context.ReservationDetails.Remove(reservationDetail);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsGroupedByRequests()
        {
            var reservationDetailsGroupedByRequests = _context.ReservationDetails
            .GroupBy(rd => rd.SpecialRequests)
            .Select(group => new ReservationDetailGroupedByRequestsDTO
            {
                SpecialRequests = group.Key,
                ReservationDetails = group.Select(rd => new ReservationDetailDTO
                {
                    ReservationId = rd.ReservationId,
                    SpecialRequests = rd.SpecialRequests,
                    LastModified = rd.LastModified,
                    BillingInformation = rd.BillingInformation
                }).ToList()
            })
        .ToList();

            return reservationDetailsGroupedByRequests;
        }

        public IEnumerable<ReservationDetailDTO> GetModifiedReservationDetails()
        {
            var modifiedReservationDetails = _context.ReservationDetails
                .Where(rd => rd.LastModified != null)
                .Select(rd => new ReservationDetailDTO
                {
                    ReservationId = rd.ReservationId,
                    SpecialRequests = rd.SpecialRequests,
                    LastModified = rd.LastModified,
                    BillingInformation = rd.BillingInformation
                })
                .ToList();

            return modifiedReservationDetails;
        }

        public IEnumerable<ReservationDetailGroupedByRequestsDTO> GetReservationDetailsWithReservations()
        {
            var reservationDetailsWithReservations = _context.ReservationDetails
                .Join(
                    _context.Reservations,
                    reservationDetail => reservationDetail.ReservationId,
                    reservation => reservation.ReservationId,
                    (reservationDetail, reservation) => new ReservationDetailGroupedByRequestsDTO
                    {
                        SpecialRequests = reservationDetail.SpecialRequests,
                        ReservationDetails = new List<ReservationDetailDTO>
                        {
                        new ReservationDetailDTO
                        {
                            ReservationId = reservationDetail.ReservationId,
                            SpecialRequests = reservationDetail.SpecialRequests,
                            LastModified = reservationDetail.LastModified,
                            BillingInformation = reservationDetail.BillingInformation
                        }
                        }
                    })
                .ToList();

            return reservationDetailsWithReservations;
        }

    }
}
