using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXPostgresMap.Models
{
    public class PostgresMessage
    {
        public string message { get; set; }
        public string topic { get; set; }
        public int offset { get; set; }
        public int partition { get; set; }
    }
}
