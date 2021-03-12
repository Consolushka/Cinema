using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Models;

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
            return View(_context.Movie.ToList());
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private Dictionary<int, List<Session>> GetSessionsByMovie()
        {
            Dictionary<int, List<Session>> resDict = new Dictionary<int, List<Session>>();
            foreach (Movie movie in _context.Movie.ToList())
            {
                resDict.Add(movie.Id, new List<Session>());
            }

            foreach(Session session in _context.Session.ToList())
            {
                resDict[session.MovieId].Add(session);
            }

            return resDict;
        }
    }
}
