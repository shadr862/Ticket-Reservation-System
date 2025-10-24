using Microsoft.EntityFrameworkCore;
using Ticket_Reservation_System_API.Model;
using Route = Ticket_Reservation_System_API.Model.Route;

namespace Ticket_Reservation_System_API.Data
{
    public class AppDbContext : DbContext
    {
        private string ConnectionString;

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options) { }

        public AppDbContext(DbContextOptionsBuilder<AppDbContext> options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Basic relationships
            modelBuilder.Entity<Bus>().HasMany(b => b.BusSchedules).WithOne(s => s.Bus).HasForeignKey(s => s.BusId);
            modelBuilder.Entity<Route>().HasMany<BusSchedule>().WithOne(s => s.Route).HasForeignKey(s => s.RouteId);
            modelBuilder.Entity<BusSchedule>().HasMany(s => s.Seats).WithOne(seat => seat.BusSchedule).HasForeignKey(seat => seat.BusScheduleId);
            modelBuilder.Entity<Ticket>().HasMany(t => t.Seats).WithOne(s => s.Ticket).HasForeignKey(s => s.TicketId);
            // Other constraints can be added as needed
        }
    }
}
