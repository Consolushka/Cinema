using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Models;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {

        private readonly MvcCinemaContext _context;

        public MoviesController(MvcCinemaContext context)
        {
            _context = context;
        }

        // GET: MoviesController
        public IActionResult Index()
        {
            ViewBag.Sessions = GetSessionsByMovie();
            ViewBag.AllowedDates = GetAllowedDates();

            byte[] encodedBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(_context.Movie.ToList()));
            ViewBag.JsonMovies = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, encodedBytes);

            return View(_context.Movie.ToList());
        }

        private Dictionary<int, List<Session>> GetSessionsByMovie()
        {
            Dictionary<int, List<Session>> resDict = new Dictionary<int, List<Session>>();
            foreach (Movie movie in _context.Movie.ToList())
            {
                resDict.Add(movie.Id, new List<Session>());
            }

            foreach (Session session in _context.Session.ToList())
            {
                resDict[session.MovieId].Add(session);
            }

            return resDict;
        }

        private List<DateTime> GetAllowedDates()
        {
            List<DateTime> resultingDates = new List<DateTime>();
            foreach (Session session in _context.Session.ToList())
            {
                if (!resultingDates.Contains(session.ShowTime.Date))
                {
                    resultingDates.Add(session.ShowTime.Date);
                }
            }
            resultingDates = resultingDates.OrderBy(d => d.Date).ToList();
            return resultingDates;
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Sessions = GetCurrentMovieSession(id);
            return View(_context.Movie.FirstOrDefault(m=>m.Id==id));
        }

        private List<Session> GetCurrentMovieSession(int id)
        {
           List<Session> resDict = new List<Session>();
            Movie currMovie = _context.Movie.FirstOrDefault(f => f.Id == id);
            foreach (Session session in _context.Session.ToList())
            {
                if (session.MovieId == currMovie.Id)
                {
                    resDict.Add(session);
                }
            }
            return resDict;
        }
    }
}
