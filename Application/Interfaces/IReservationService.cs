using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReservationservice
    {
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task AddReservationAsync(CreateReservationDto reservationDto);
        Task UpdateReservationAsync(UpdateReservationDto reservationDto);
        Task DeleteReservationAsync(int id);
    }
}
