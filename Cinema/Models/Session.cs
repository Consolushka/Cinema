using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Session
    {
        public int Id { get; set; }
        public Movie Movie{ get; set; }
        public Hall Hall { get; set; }
        public DateTime ShowTime { get; set; }
    }
}
