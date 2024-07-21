using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task AddPaymentAsync(CreatePaymentDto paymentDto);
        Task UpdatePaymentAsync(UpdatePaymentDto paymentDto);
        Task DeletePaymentAsync(int id);
    }
}
