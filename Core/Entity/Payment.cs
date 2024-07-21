using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PoolTableId { get; set; }
        public PoolTable PoolTable { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public bool IsCompleted { get; set; }
    }
}
