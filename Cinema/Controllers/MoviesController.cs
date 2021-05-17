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
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                resDict[session.MovieId] = resDict[session.MovieId].OrderBy(s => s.ShowTime).ToList();
            }

            return resDict;
        }

        private List<string> GetAllowedDates()
        {
            List<DateTime> datesList = new List<DateTime>();
            List<string> resultingList = new List<string>();
            foreach (Session session in _context.Session.ToList())
            {
                if (!datesList.Contains(session.ShowTime.Date))
                {
                    datesList.Add(session.ShowTime.Date);
                }
            }
            datesList = datesList.OrderBy(d => d.Date).ToList();
            foreach (DateTime currentDate in datesList)
            {
                string date = currentDate.Day.ToString() + "." + currentDate.Month.ToString() + "." + currentDate.Year.ToString();
                resultingList.Add(date);
            }
            return resultingList;
        }

        public void AddComment()
        {
            StreamReader sr = new StreamReader(Request.Body);
            Comment comment = JsonSerializer.Deserialize<Comment>(sr.ReadToEnd());
            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Sessions = GetCurrentMovieSession(id);
            ViewBag.Comments = GetCurrentMovieComments(id);
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

        private List<Comment> GetCurrentMovieComments(int id)
        {
            List<Comment> resDict = new List<Comment>();
            Movie currMovie = _context.Movie.FirstOrDefault(f => f.Id == id);
            foreach (Comment comment in _context.Comment.ToList())
            {
                if (comment.MovieId == currMovie.Id)
                {
                    resDict.Add(comment);
                }
            }
            return resDict;
        }
    }
}
