using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PoolTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CreatePoolTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdatePoolTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
