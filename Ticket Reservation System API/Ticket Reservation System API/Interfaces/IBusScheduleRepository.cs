using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IBusScheduleRepository
    {
        Task<BusSchedule?> GetByIdAsync(Guid id);
        Task<List<BusSchedule>> GetSchedulesByRouteAndDateAsync(string from, string to, DateTime date);
        Task AddAsync(BusSchedule schedule);
    }
}
