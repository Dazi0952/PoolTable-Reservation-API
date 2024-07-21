using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PoolTableId { get; set; }
        public int ReservationId { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class CreatePaymentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PoolTableId { get; set; }
        public int ReservationId { get; set; }
    }

    public class UpdatePaymentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PoolTableId { get; set; }
        public int ReservationId { get; set; }
        public bool IsCompleted { get; set; }
    }

}
