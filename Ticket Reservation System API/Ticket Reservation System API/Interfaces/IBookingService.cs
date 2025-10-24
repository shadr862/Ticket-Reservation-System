using Ticket_Reservation_System_API.Dtos;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IBookingService
    {
        Task<SeatPlanDto> GetSeatPlanAsync(System.Guid busScheduleId);
        Task<BookSeatResultDto> BookSeatAsync(BookSeatInputDto input);
    }
}
