using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetSeatsByScheduleIdAsync(Guid scheduleId);
        Task<IEnumerable<Seat>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task UpdateAsync(Seat seat);
    }
}
