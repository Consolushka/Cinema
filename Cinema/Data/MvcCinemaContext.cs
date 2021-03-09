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

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Hall> Halls{ get; set; }
        public DbSet<Seat> Seats{ get; set; }
        public DbSet<Session> Sessions{ get; set; }
        public DbSet<Reservation> Reservations{ get; set; }
    }
}