using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;

namespace Cinema.Controllers
{
    public class MovieSessions : Controller
    {

        private readonly MvcCinemaContext _context;

        public MovieSessions(MvcCinemaContext context)
        {
            _context = context;
        }

        public IActionResult Index(int ?id)
        {
            return View();
        }
    }
}
