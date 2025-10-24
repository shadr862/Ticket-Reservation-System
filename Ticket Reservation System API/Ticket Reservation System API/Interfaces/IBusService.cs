using Microsoft.AspNetCore.Mvc;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IBusService
    {
        Task<List<FullBusResultDto>> GetAllBuses();
        Task<FullBusResultDto> AddFullBusAsync(AddFullBusDto dto);
   

    }
}
