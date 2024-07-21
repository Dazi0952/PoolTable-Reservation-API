using Application.DTOs;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPoolTableRepository _pooltableRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IReservationRepository reservationRepository,
            IPoolTableRepository pooltableRepository,
            ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
            _pooltableRepository = pooltableRepository;
            _logger = logger;
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return new PaymentDto
            {
                Id = payment.Id,
                Description = payment.Description,
                PoolTableId = payment.PoolTableId,
                ReservationId = payment.ReservationId,
                IsCompleted = payment.IsCompleted
            };
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Select(t => new PaymentDto
            {
                Id = t.Id,
                Description = t.Description,
                PoolTableId = t.PoolTableId,
                ReservationId = t.ReservationId,
                IsCompleted = t.IsCompleted
            });
        }

        public async Task AddPaymentAsync(CreatePaymentDto paymentDto)
        {
            _logger.LogInformation($"Attempting to add Payment with ReservationId: {paymentDto.ReservationId} and PoolTableId: {paymentDto.PoolTableId}");

            var reservation = await _reservationRepository.GetByIdAsync(paymentDto.ReservationId);
            if (reservation == null)
            {
                _logger.LogError($"Reservation with Id {paymentDto.ReservationId} not found.");
                throw new Exception("Invalid ReservationId");
            }

            var pooltable = await _pooltableRepository.GetByIdAsync(paymentDto.PoolTableId);
            if (pooltable == null)
            {
                _logger.LogError($"PoolTable with Id {paymentDto.PoolTableId} not found.");
                throw new Exception("Invalid PoolTableId");
            }

            var payment = new Payment
            {
                Description = paymentDto.Description,
                PoolTableId = paymentDto.PoolTableId,
                ReservationId = paymentDto.ReservationId,
                IsCompleted = false
            };

            _logger.LogInformation($"Adding Payment: {payment.Description}, ReservationId: {payment.ReservationId}, PoolTableId: {payment.PoolTableId}");
            await _paymentRepository.AddAsync(payment);
        }

        public async Task UpdatePaymentAsync(UpdatePaymentDto paymentDto)
        {
            _logger.LogInformation($"Updating Payment with Id: {paymentDto.Id}, ReservationId: {paymentDto.ReservationId}, PoolTableId: {paymentDto.PoolTableId}");

            var payment = await _paymentRepository.GetByIdAsync(paymentDto.Id);
            if (payment != null)
            {
                var reservation = await _reservationRepository.GetByIdAsync(paymentDto.ReservationId);
                var pooltable = await _pooltableRepository.GetByIdAsync(paymentDto.PoolTableId);

                if (reservation == null)
                {
                    _logger.LogError($"Invalid ReservationId: {paymentDto.ReservationId}");
                    throw new Exception("Invalid ReservationId");
                }

                if (pooltable == null)
                {
                    _logger.LogError($"Invalid PoolTableId: {paymentDto.PoolTableId}");
                    throw new Exception("Invalid PoolTableId");
                }

                payment.Description = paymentDto.Description;
                payment.PoolTableId = paymentDto.PoolTableId;
                payment.ReservationId = paymentDto.ReservationId;
                payment.IsCompleted = paymentDto.IsCompleted;
                await _paymentRepository.UpdateAsync(payment);
            }
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment != null)
            {
                await _paymentRepository.DeleteAsync(id);
            }
        }
    }
}
