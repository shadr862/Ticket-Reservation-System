using Microsoft.EntityFrameworkCore;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly AppDbContext _db;
        public SeatRepository(AppDbContext db) => _db = db;

        public Task<IEnumerable<Seat>> GetSeatsByScheduleIdAsync(Guid scheduleId) =>
            _db.Seats.Where(s => s.BusScheduleId == scheduleId).ToListAsync().ContinueWith(t => (IEnumerable<Seat>)t.Result);

        public Task<IEnumerable<Seat>> GetByIdsAsync(IEnumerable<Guid> ids) =>
            _db.Seats.Where(s => ids.Contains(s.Id)).ToListAsync().ContinueWith(t => (IEnumerable<Seat>)t.Result);

        public Task UpdateAsync(Seat seat)
        {
            _db.Seats.Update(seat);
            return Task.CompletedTask;
        }
    }
}
