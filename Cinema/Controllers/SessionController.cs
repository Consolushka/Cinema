using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cinema.Controllers
{
    public class SessionController : Controller
    {

        private readonly MvcCinemaContext _context;

        private Session session { get; set; }

        public SessionController(MvcCinemaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {

            session = await _context.Session.FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            ViewBag.Movie = _context.Movie.SingleOrDefault(m => m.Id == session.MovieId);
            ViewBag.Hall = _context.Hall.SingleOrDefault(h => h.Id == session.HallId);
            ViewBag.Seats = GetFullSessionSeats();
            return View(session);
        }

        public void CreateReservation()
        {
            StreamReader sr = new StreamReader(Request.Body);
            UserReservation data = JsonSerializer.Deserialize<UserReservation>(sr.ReadToEnd());
            foreach(int seatId in data.ReservedSeats)
            {
                Reservation res = new Reservation();
                res.SeatId = seatId;
                res.SessionId = data.Session;
                res.PersonFirstName = data.FName;
                res.PersonSecondName = data.SName;
                _context.Reservation.Add(res);
            }
            _context.SaveChanges();
        }

        private Dictionary<int, List<SessionSeat>> GetFullSessionSeats()
        {
            List<SessionSeat> seatsList = new List<SessionSeat>();

            foreach (Seat seat in _context.Seat.ToList())
            {
                seatsList.Add(CreateSessionSeat(seat));
            }

            return GetFormatedSeatList(seatsList);

        }

        private SessionSeat CreateSessionSeat(Seat seat)
        {
            SessionSeat sessionSeat = new SessionSeat(seat, session,_context);

            return sessionSeat;
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