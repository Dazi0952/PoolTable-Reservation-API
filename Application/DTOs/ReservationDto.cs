using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateReservationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
