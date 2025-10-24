using Route = Ticket_Reservation_System_API.Model.Route;
namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IRouteRepository
    {
        Task<Route?> GetByFromToAsync(string from, string to);
        Task AddAsync(Route route);
    }
}
