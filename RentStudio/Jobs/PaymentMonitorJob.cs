using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Quartz;
using RentStudio.DataAccesLayer;
using RentStudio.Repositories.PaymentQueueMessagesRepository;
using RentStudio.Services.EmailService;
using RentStudio.Services.PaymentQueueMessagesService;
using RentStudio.Services.PaymentService;
using RentStudio.Services.UserService;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace RentStudio.Jobs
{
    public class PaymentMonitorJob : IJob
    {
        private readonly EmailService _emailService;
        private readonly IPaymentQueueMessagesService _paymentQueueMessagesService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly RentDbContext _rentDbContext;

        private readonly int batchSize = 2;

        public PaymentMonitorJob(EmailService emailService, IPaymentQueueMessagesService paymentQueueMessagesService, IUserService userService, IPaymentService paymentService, RentDbContext rentDbContext)
        {
            _emailService = emailService;
            _paymentQueueMessagesService = paymentQueueMessagesService;
            _userService = userService;
            _paymentService = paymentService;
            _rentDbContext = rentDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var unprocessedMessage = await _paymentQueueMessagesService.GetQueueUnprocessedMessagesAsync(batchSize);
                //var unprocessedMessage aduce un nr de elem din batchSize=2
                foreach (var message in unprocessedMessage)
                {
                    var user = await _userService.GetById(message.UserId);
                    var paymentAmount = await _paymentService.GetPaymentAmount(message.UserId, message.ReservationId);
                    var paymentStatus = await _paymentService.CheckPaymentStatusAsync(message.UserId, message.ReservationId);
                    await _emailService.SendEmailWithAttachmentAsync(
                                toEmail: user.Email,
                                subject: $"Payment - {paymentStatus} - {user.FirstName + " " + user.LastName}",
                                body: $"Payment with amount: {paymentAmount} for reservation {message.ReservationId} with status {paymentStatus} has been processed"
                                );
                    message.Processed = true;
                    message.ProcessedDate = DateTime.UtcNow;
                }
                await _rentDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or handle errors)
                Console.WriteLine($"Error reading the payment db: {ex.Message}");
            }

        }
    }
}
