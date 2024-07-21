using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto paymentDto)
        {
            try
            {
                _logger.LogInformation($"Received request to create Payment with ReservationId: {paymentDto.ReservationId} and PoolTableId: {paymentDto.PoolTableId}");
                await _paymentService.AddPaymentAsync(paymentDto);
                _logger.LogInformation($"Successfully created Payment with Description: {paymentDto.Description}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Payment");
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] UpdatePaymentDto paymentDto)
        {
            if (paymentDto == null || id != paymentDto.Id)
            {
                return BadRequest();
            }
            await _paymentService.UpdatePaymentAsync(paymentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
