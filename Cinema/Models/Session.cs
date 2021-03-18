using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie{ get; set; }
        public int HallId{ get; set; }
        public Hall Hall{ get; set; }
        public DateTime ShowTime { get; set; }
    }
}
