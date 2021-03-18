using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;

namespace Cinema.Models
{
    public class SessionSeat:Seat
    {
        public bool IsReserved { get; set; }

        public SessionSeat(Seat seat, Session session, MvcCinemaContext context)
        {
            Id = seat.Id;
            Row = seat.Row;
            Number = seat.Number;

            Reservation reservation = context.Reservation.SingleOrDefault(r => r.SeatId == seat.Id && r.SessionId == session.Id);

            if (reservation != null)
            {
                IsReserved = true;
            }
            else
            {
                IsReserved = false;
            }
        }
    }
}
