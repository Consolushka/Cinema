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

            UserSession userSession = new UserSession(session, _context);

            return View(userSession);
        }

        public void CreateReservation()
        {
            StreamReader sr = new StreamReader(Request.Body);
            UserReservation data = JsonSerializer.Deserialize<UserReservation>(sr.ReadToEnd());
            foreach (int seatId in data.ReservedSeats)
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
    }
}