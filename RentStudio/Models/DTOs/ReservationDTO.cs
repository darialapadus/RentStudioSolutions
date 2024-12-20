﻿using RentStudio.DataAccesLayer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentStudio.Models.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfRooms { get; set; }
        public int NumberOfGuests { get; set; }

        [Required]
        public string Status { get; set; } = "";

        public string PaymentMethod { get; set; } = "";

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public ReservationDetailDTO ReservationDetail { get; set; }
    }
    public class GroupedReservationsByStatusDTO
    {
        public string Status { get; set; }
        public List<ReservationDTO> Reservations { get; set; }
    }
    public class UpdateReservationDTO
    {
        public int ReservationId { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal NewAmount { get; set; }
    }
}
