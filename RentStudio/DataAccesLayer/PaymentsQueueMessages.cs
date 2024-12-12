using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentStudio.DataAccesLayer
{
    public class PaymentsQueueMessage
    {
        [Key]
        public int PaymentsQueueId { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }

        public bool Processed { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public DateTime InsertDate { get; set; }

        [ForeignKey("PaymentId")]
        public int PaymentId {  get; set; }

    }
}
