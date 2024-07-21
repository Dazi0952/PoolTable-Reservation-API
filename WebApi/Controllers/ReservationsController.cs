using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.DTOs;
using Application.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationservice _reservationservice;

        public ReservationsController(IReservationservice reservationservice)
        {
            _reservationservice = reservationservice;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationservice.GetReservationByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationservice.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto reservationDto)
        {
            if (reservationDto == null)
            {
                return BadRequest();
            }
            await _reservationservice.AddReservationAsync(reservationDto);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservationDto.Id }, reservationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto reservationDto)
        {
            if (reservationDto == null || id != reservationDto.Id)
            {
                return BadRequest();
            }
            await _reservationservice.UpdateReservationAsync(reservationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationservice.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
