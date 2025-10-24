using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Services
{
    public class SearchService:ISearchService
    {
        private readonly IBusScheduleRepository _scheduleRepo;

        public SearchService(IBusScheduleRepository scheduleRepo)
        {
            _scheduleRepo = scheduleRepo;
        }

        public async Task<List<AvailableBusDto>> SearchAvailableBusesAsync(string from, string to, DateTime journeyDate)
        {
            var schedules = await _scheduleRepo.GetSchedulesByRouteAndDateAsync(from, to, journeyDate.Date);

            var list = schedules.Select(s => new AvailableBusDto
            {
                BusScheduleId = s.Id,
                CompanyName = s.Bus.CompanyName,
                BusName = s.Bus.BusName,
                StartTime = s.StartTime,
                ArrivalTime = s.ArrivalTime,
                Price = s.Price,
                From = s.Route.From,
                To = s.Route.To,
                SeatsLeft = s.Seats.Count(x => x.Status == SeatStatus.Available)
            }).ToList();

            return list;
        }
    }
}
