using Ticket_Reservation_System_API.Dtos;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface ISearchService
    {
        Task<List<AvailableBusDto>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate);
    }
}
