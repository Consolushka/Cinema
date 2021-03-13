using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class SessionController : Controller
    {

        private readonly MvcCinemaContext _context;

        public SessionController(MvcCinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            List<SessionSeat> seatsList = new List<SessionSeat>();

            var session = await _context.Session.FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }
            
            foreach(Seat seat in _context.Seat.ToList())
            {
                seatsList.Add(CreateSessionSeat(seat));
            }

            seatsList = seatsList.OrderBy(x => x.Row).ThenBy(x => x.Number).ToList();

            var resSeats = GetSeatsByRows(seatsList);

            ViewBag.Movie = _context.Movie.SingleOrDefault(m => m.Id == session.MovieId);

            ViewBag.Seats = resSeats;
            return View();
        }

        private SessionSeat CreateSessionSeat(Seat seat)
        {
            SessionSeat sessionSeat = new SessionSeat(seat);

            Reservation reservation = _context.Reservation.SingleOrDefault(r => r.SeatId == seat.Id);

            if (reservation != null)
            {
                sessionSeat.IsReserved = true;
            }
            else
            {
                sessionSeat.IsReserved = false;
            }

            return sessionSeat;
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
