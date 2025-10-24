using Microsoft.AspNetCore.Mvc;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;
using Route = Ticket_Reservation_System_API.Model.Route;

namespace Ticket_Reservation_System_API.Services
{
    public class BusService:IBusService
    {
        private readonly IBusRepository _busRepo;
        private readonly IRouteRepository _routeRepo;
        private readonly IBusScheduleRepository _scheduleRepo;
        private readonly AppDbContext _db;
        public BusService(AppDbContext db,IBusRepository busRepo,IRouteRepository routeRepo,IBusScheduleRepository scheduleRepo)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _busRepo = busRepo ?? throw new ArgumentNullException(nameof(busRepo));
            _routeRepo = routeRepo ?? throw new ArgumentNullException(nameof(routeRepo));
            _scheduleRepo = scheduleRepo ?? throw new ArgumentNullException(nameof(scheduleRepo));
        }

       
        public async Task<FullBusResultDto> AddFullBusAsync(AddFullBusDto dto)
        {
            if (dto.TotalSeats <= 0)
                throw new ArgumentException("TotalSeats must be greater than zero.");
            if (string.IsNullOrWhiteSpace(dto.From) || string.IsNullOrWhiteSpace(dto.To))
                throw new ArgumentException("From and To cannot be empty.");

            using var trx = await _db.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Route
                var route = await _routeRepo.GetByFromToAsync(dto.From, dto.To);
                if (route == null)
                {
                    route = new Route
                    {
                        Id = Guid.NewGuid(),
                        From = dto.From.Trim(),
                        To = dto.To.Trim(),
                        DistanceKm = 0
                    };
                    await _routeRepo.AddAsync(route);
                }

                // Step 2: Bus
                var bus = new Bus
                {
                    Id = Guid.NewGuid(),
                    CompanyName = dto.CompanyName.Trim(),
                    BusName = dto.BusName.Trim(),
                    TotalSeats = dto.TotalSeats
                };
                await _busRepo.AddAsync(bus);

                // Step 3: Schedule
                var schedule = new BusSchedule
                {
                    Id = Guid.NewGuid(),
                    BusId = bus.Id,
                    RouteId = route.Id,
                    JourneyDate = dto.JourneyDate.Date,
                    StartTime = dto.StartTime,
                    ArrivalTime = dto.ArrivalTime,
                    Price = dto.Price,
                    Seats = new List<Seat>()
                };

                // Step 4: Generate Seats
                for (int i = 1; i <= dto.TotalSeats; i++)
                {
                    schedule.Seats.Add(new Seat
                    {
                        Id = Guid.NewGuid(),
                        Number = i,
                        Row = ((i - 1) / 4) + 1,
                        Status = (i%4==0)? SeatStatus.Sold : SeatStatus.Available
                    });
                }

                await _scheduleRepo.AddAsync(schedule);
                await _db.SaveChangesAsync();
                await trx.CommitAsync();

                return new FullBusResultDto
                {
                    BusId = bus.Id,
                    BusName = bus.BusName,
                    CompanyName = bus.CompanyName,
                    TotalSeats = bus.TotalSeats,
                    RouteId = route.Id,
                    From = route.From,
                    To = route.To,
                    ScheduleId = schedule.Id,
                    JourneyDate = schedule.JourneyDate,
                    StartTime = schedule.StartTime,
                    ArrivalTime = schedule.ArrivalTime,
                    Price = schedule.Price,
                    SeatsCreated = schedule.Seats.Count
                };
            }
            catch
            {
                await trx.RollbackAsync();
                throw;
            }
        }
        
        public async Task<List<FullBusResultDto>> GetAllBuses()
        {
            var buses = await _busRepo.GetAllAscync();
            return buses; // Wrap list in Ok()
        }

    }
}
