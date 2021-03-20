using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;

namespace Cinema.Models
{
    public class UserSession : Session
    {
        public Dictionary<int, List<SessionSeat>> sessionSeats { get; set;}

        private MvcCinemaContext _context { get; set;}

        public UserSession(Session session, MvcCinemaContext context)
        {
            _context = context;

            Id = session.Id;
            HallId = session.HallId;
            MovieId = session.MovieId;
            ShowTime = session.ShowTime;
            Hall = _context.Hall.SingleOrDefault(h => h.Id == HallId);
            Movie = _context.Movie.SingleOrDefault(m => m.Id == MovieId);
            sessionSeats = GetFullSessionSeats();
        }

        private Dictionary<int, List<SessionSeat>> GetFullSessionSeats()
        {
            List<SessionSeat> seatsList = new List<SessionSeat>();

            foreach (Seat seat in _context.Seat.ToList())
            {
                seatsList.Add(new SessionSeat(seat, this, _context));
            }

            return GetFormatedSeatList(seatsList);

        }

        private Dictionary<int, List<SessionSeat>> GetFormatedSeatList(List<SessionSeat> unformatted)
        {
            List<SessionSeat> listByRowsAndNumber = new List<SessionSeat>();

            listByRowsAndNumber = unformatted.OrderBy(x => x.Row).ThenBy(x => x.Number).ToList();

            var formattedList = GetSeatsByRows(listByRowsAndNumber);

            return formattedList;
        }

        private Dictionary<int, List<SessionSeat>> GetSeatsByRows(List<SessionSeat> seatsList)
        {
            Dictionary<int, List<SessionSeat>> seatsByRow = new Dictionary<int, List<SessionSeat>>();
            foreach (SessionSeat seat in seatsList)
            {
                if (seatsByRow.ContainsKey(seat.Row))
                {
                    seatsByRow[seat.Row].Add(seat);
                }
                else
                {
                    seatsByRow.Add(seat.Row, new List<SessionSeat> { seat });
                }
            }

            return seatsByRow;
        }
    }
}
