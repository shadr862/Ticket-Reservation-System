using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IBusRepository
    {
        Task AddAsync(Bus bus);
        Task<List<FullBusResultDto>> GetAllAscync();
    }
}
