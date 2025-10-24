using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IPassengerRepository
    {
        Task AddAsync(Passenger passenger);
    }
}
