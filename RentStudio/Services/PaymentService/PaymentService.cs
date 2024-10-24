﻿using Microsoft.AspNetCore.Http.HttpResults;
using RentStudio.DataAccesLayer;
using RentStudio.Helpers;
using RentStudio.Models.DTOs;
using RentStudio.Models.Enums;
using RentStudio.Repositories.PaymentRepository;
using RentStudio.Repositories.ReservationRepository;
using RentStudio.Repositories.UserRepository;
using RentStudio.Services.UserService;
using static RentStudio.Helpers.Constants;

namespace RentStudio.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserService _userService;
        private readonly IReservationRepository _reservationRepository;
        public PaymentService(IPaymentRepository paymentRepository, IUserService userService, IReservationRepository reservationRepository)
        {
            _paymentRepository = paymentRepository;
            _userService = userService;
            _reservationRepository = reservationRepository;

        }

        public async Task ProcessPaymentAsync(PaymentDTO paymentDTO)
        {
            var userId = _userService.PartialRegisterUser(paymentDTO);
            //_paymentProcessingService.ProcessPayment(paymentDTO, userId);
            var payment = new Payment
            {
                UserId = userId,
                ReservationId = paymentDTO.ReservationId,
                Amount = paymentDTO.Amount,
                Status = "Pending",
                TransactionDate = DateTime.UtcNow
            };
            await SavePaymentAsync(payment, true);

            var paymentValidated = new Payment
            {
                UserId = userId,
                ReservationId = paymentDTO.ReservationId,
                Amount = paymentDTO.Amount,
                Status = await ValidatePaymentAsync(paymentDTO),
                TransactionDate = DateTime.UtcNow
            };

            await SavePaymentAsync(paymentValidated, true);

            payment.Status = "Processed";

            await SavePaymentAsync(payment, false);
        }
        public async Task SavePaymentAsync(Payment payment, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    await _paymentRepository.AddAsync(payment);
                }
                await _paymentRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the payment.", ex);
            }
        }
        public async Task<string> ValidatePaymentAsync(PaymentDTO paymentDTO)
        {
            var status = await BankSimulator.BankProcessPaymentAsync();
            return status;
        }
        public async Task<string> CheckPaymentStatusAsync(Guid userId, int reservationId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByUserIdAsync(userId, reservationId);

                if (payment == null)
                {
                    return Constants.ErrorMessages.PaymentNotFound;
                }
                return payment.Status;
                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while checking payment status.", ex);
            }
        }
        public List<PaymentDetailsDTO> GetPaymentsByUserId(Guid userId)
        {
            var payments = _paymentRepository.GetPaymentsByUserId(userId);
            var paymentDetails = payments.Where(p => p.Status != PaymentStatus.Processed.ToString()).Select(p => new PaymentDetailsDTO
            {
                NumberOfRooms = p.Reservation?.NumberOfRooms ?? 0,
                PaymentDate = p.TransactionDate,
                Amount = p.Amount,
                Status = p.Status
            }).ToList();

            return paymentDetails;
        }
        public async Task<string> RefundPaymentAsync(Guid userId, int reservationId)
        {
            var payments = await _paymentRepository.GetPaymentsAsync(userId, reservationId);
            var succeededRefunds = payments.FirstOrDefault(p => p.Status == PaymentStatus.Succeeded.ToString());
            if (succeededRefunds == null)
            {
                return PaymentMessage.NoPaymentFound; 
            }
            var paymentsRefunds = payments.FirstOrDefault(p => p.Status == PaymentStatus.Refund.ToString());
            if (paymentsRefunds != null)
            {
                return PaymentMessage.RefundAlreadyProcessed; 
            }
            var refundPayment = new Payment
            {
                UserId = userId,
                ReservationId = reservationId,
                Amount = succeededRefunds.Amount,
                Status = PaymentStatus.Refund.ToString(), 
                TransactionDate = DateTime.UtcNow
            };
            await _paymentRepository.AddAsync(refundPayment);
            await _paymentRepository.SaveAsync();

            return PaymentMessage.RefundProcessed;
        }
       
    }

}
