using Microsoft.EntityFrameworkCore;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Ticket_Reservation_System_API.Repositories
{
    public class BusRepository:IBusRepository
    {
        private readonly AppDbContext _db;
       

       public BusRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Bus bus)
        {
            await _db.Buses.AddAsync(bus);
            await _db.SaveChangesAsync();
        }

       

        public async Task<List<FullBusResultDto>> GetAllAscync()
        {
            var buses = _db.BusSchedules
               .Include(s => s.Bus)
               .Include(s => s.Route)
               .Include(s => s.Seats)
               .Select(s => new FullBusResultDto
            {
                BusId = s.BusId,
                BusName = s.Bus.BusName,
                CompanyName = s.Bus.CompanyName,
                TotalSeats = s.Bus.TotalSeats,
                RouteId = s.RouteId,
                From = s.Route.From,
                To = s.Route.To,
                ScheduleId = s.Id,
                JourneyDate = s.JourneyDate,
                StartTime = s.StartTime,
                ArrivalTime = s.ArrivalTime,
                Price = s.Price,
                SeatsCreated = s.Seats.Count
            }).ToList();

            return buses;
        }


    }
}
