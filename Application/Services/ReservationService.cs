using Application.DTOs;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces;


namespace Application.Services
{
    public class Reservationservice : IReservationservice
    {
        private readonly IReservationRepository _reservationRepository;

        public Reservationservice(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            return new ReservationDto { Id = reservation.Id, Name = reservation.Name };
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(p => new ReservationDto { Id = p.Id, Name = p.Name });
        }

        public async Task AddReservationAsync(CreateReservationDto reservationDto)
        {
            var reservation = new Reservation
            {
                Id = reservationDto.Id,
                Name = reservationDto.Name
            };
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservationAsync(UpdateReservationDto reservationDto)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationDto.Id);
            if (reservation != null)
            {
                reservation.Name = reservationDto.Name;
                await _reservationRepository.UpdateAsync(reservation);
            }
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
        }
    }
}
