using Microsoft.EntityFrameworkCore;
using Cinema.Models;

namespace Cinema.Data
{
    public class MvcCinemaContext : DbContext
    {
        public MvcCinemaContext(DbContextOptions<MvcCinemaContext> options)
            : base(options)
        {
        }
    }
}