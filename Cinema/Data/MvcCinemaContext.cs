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

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Hall> Hall{ get; set; }
        public DbSet<Seat> Seat{ get; set; }
        public DbSet<Session> Session{ get; set; }
        public DbSet<Reservation> Reservation{ get; set; }
    }
}