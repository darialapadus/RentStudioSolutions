using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Services.PaymentService;

namespace RentStudio.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult>AddPayment([FromBody] PaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _paymentService.ProcessPaymentAsync(paymentDTO); 
            return Ok(new { Message = "Payment processed successfully." });
        }

        [HttpGet]
        public async Task<ActionResult<string>> CheckPaymentStatus([FromQuery] Guid userId, [FromQuery] int reservationId)
        {
            var status = await _paymentService.CheckPaymentStatusAsync(userId, reservationId);
            return Ok(status);
        }

        [HttpGet("user-payments/{userId}")]
        public IActionResult GetUserPayments(Guid userId)
        {
            var payments = _paymentService.GetPaymentsByUserId(userId);
            return Ok(payments);
        }

        [HttpPost("refund-payments")]
        public async Task<ActionResult<string>> PaymentRefund([FromQuery] Guid userId, [FromQuery] int reservationId)
        { 
            var refundStatus = await _paymentService.RefundPaymentAsync(userId, reservationId);
            return Ok(refundStatus);

        }


        
    }

}
