using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Core.Entity;
using Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace WebApi.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentRepository> _mockPaymentRepository;
        private readonly Mock<IReservationRepository> _mockReservationRepository;
        private readonly Mock<IPoolTableRepository> _mockPoolTableRepository;
        private readonly Mock<ILogger<PaymentService>> _mockLogger;
        private readonly PaymentService _service;

        public PaymentServiceTests()
        {
            _mockPaymentRepository = new Mock<IPaymentRepository>();
            _mockReservationRepository = new Mock<IReservationRepository>();
            _mockPoolTableRepository = new Mock<IPoolTableRepository>();
            _mockLogger = new Mock<ILogger<PaymentService>>();

            _service = new PaymentService(
                _mockPaymentRepository.Object,
                _mockReservationRepository.Object,
                _mockPoolTableRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task AddPaymentAsync_ShouldThrowException_WhenReservationDoesNotExist()
        {
            // Arrange
            var createPaymentDto = new CreatePaymentDto { Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Reservation)null);

            // Act
            Func<Task> act = async () => await _service.AddPaymentAsync(createPaymentDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid ReservationId");
        }

        [Fact]
        public async Task AddPaymentAsync_ShouldThrowException_WhenPoolTableDoesNotExist()
        {
            // Arrange
            var createPaymentDto = new CreatePaymentDto { Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var reservation = new Reservation { Id = 1, Name = "Reservation1" };
            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PoolTable)null);

            // Act
            Func<Task> act = async () => await _service.AddPaymentAsync(createPaymentDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid PoolTableId");
        }

        [Fact]
        public async Task AddPaymentAsync_ShouldCallRepository_WhenReservationAndPoolTableExist()
        {
            // Arrange
            var createPaymentDto = new CreatePaymentDto { Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var reservation = new Reservation { Id = 1, Name = "Reservation1" };
            var pooltable = new PoolTable { Id = 1, Name = "PoolTable1" };

            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pooltable);

            // Act
            await _service.AddPaymentAsync(createPaymentDto);

            // Assert
            _mockPaymentRepository.Verify(x => x.AddAsync(It.Is<Payment>(t => t.Description == createPaymentDto.Description && t.ReservationId == createPaymentDto.ReservationId && t.PoolTableId == createPaymentDto.PoolTableId)), Times.Once);
        }

        [Fact]
        public async Task UpdatePaymentAsync_ShouldThrowException_WhenReservationDoesNotExist()
        {
            // Arrange
            var updatePaymentDto = new UpdatePaymentDto { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            _mockPaymentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Payment());
            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Reservation)null);

            // Act
            Func<Task> act = async () => await _service.UpdatePaymentAsync(updatePaymentDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid ReservationId");
        }

        [Fact]
        public async Task UpdatePaymentAsync_ShouldThrowException_WhenPoolTableDoesNotExist()
        {
            // Arrange
            var updatePaymentDto = new UpdatePaymentDto { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var payment = new Payment { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var reservation = new Reservation { Id = 1, Name = "Reservation1" };

            _mockPaymentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(payment);
            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PoolTable)null);

            // Act
            Func<Task> act = async () => await _service.UpdatePaymentAsync(updatePaymentDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid PoolTableId");
        }

        [Fact]
        public async Task UpdatePaymentAsync_ShouldCallRepository_WhenReservationAndPoolTableExist()
        {
            // Arrange
            var updatePaymentDto = new UpdatePaymentDto { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var payment = new Payment { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };
            var reservation = new Reservation { Id = 1, Name = "Reservation1" };
            var pooltable = new PoolTable { Id = 1, Name = "PoolTable1" };

            _mockPaymentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(payment);
            _mockReservationRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pooltable);

            // Act
            await _service.UpdatePaymentAsync(updatePaymentDto);

            // Assert
            _mockPaymentRepository.Verify(x => x.UpdateAsync(It.Is<Payment>(t => t.Description == updatePaymentDto.Description && t.ReservationId == updatePaymentDto.ReservationId && t.PoolTableId == updatePaymentDto.PoolTableId)), Times.Once);
        }

        [Fact]
        public async Task DeletePaymentAsync_ShouldCallRepository_WhenPaymentExists()
        {
            // Arrange
            var payment = new Payment { Id = 1, Description = "Task1", ReservationId = 1, PoolTableId = 1 };

            _mockPaymentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(payment);

            // Act
            await _service.DeletePaymentAsync(1);

            // Assert
            _mockPaymentRepository.Verify(x => x.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeletePaymentAsync_ShouldNotCallRepository_WhenPaymentDoesNotExist()
        {
            // Arrange
            _mockPaymentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Payment)null);

            // Act
            await _service.DeletePaymentAsync(1);

            // Assert
            _mockPaymentRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
