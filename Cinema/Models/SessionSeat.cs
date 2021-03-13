using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class SessionSeat
    {
        public int Id { get; set;}
        public int Row { get; set; }
        public int Number { get; set; }

        public bool IsReserved { get; set; }

        public SessionSeat(Seat seat)
        {
            this.Id = seat.Id;
            this.Row = seat.Row;
            this.Number = seat.Number;
        }
    }
}
