using Microsoft.EntityFrameworkCore;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Repositories
{
    public class BusScheduleRepository : IBusScheduleRepository
    {
        private readonly AppDbContext _db;
        public BusScheduleRepository(AppDbContext db) => _db = db;

        public Task<BusSchedule?> GetByIdAsync(Guid id) =>
            _db.BusSchedules.Include(s => s.Bus).Include(s => s.Route).Include(s => s.Seats).FirstOrDefaultAsync(s => s.Id == id);

        public Task<List<BusSchedule>> GetSchedulesByRouteAndDateAsync(string from, string to, DateTime date) =>
            _db.BusSchedules
               .Include(s => s.Bus)
               .Include(s => s.Route)
               .Include(s => s.Seats)
               .Where(s => s.JourneyDate == date && s.Route.From == from && s.Route.To == to)
               .ToListAsync();


        public async Task AddAsync(BusSchedule schedule)
        {
            await _db.BusSchedules.AddAsync(schedule);
            await _db.SaveChangesAsync();
        }
    }
}
