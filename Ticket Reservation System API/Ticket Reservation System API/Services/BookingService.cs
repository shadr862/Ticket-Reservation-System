using Ticket_Reservation_System_API.Dtos;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Services
{
    public class BookingService : IBookingService
    {
        private readonly ISeatRepository _seatRepo;
        private readonly IBusScheduleRepository _scheduleRepo;
        private readonly ITicketRepository _ticketRepo;
        private readonly IPassengerRepository _passengerRepo;
        private readonly IUnitOfWork _uow;

        public BookingService(ISeatRepository seatRepo, IBusScheduleRepository scheduleRepo,
            ITicketRepository ticketRepo, IPassengerRepository passengerRepo, IUnitOfWork uow)
        {
            _seatRepo = seatRepo;
            _scheduleRepo = scheduleRepo;
            _ticketRepo = ticketRepo;
            _passengerRepo = passengerRepo;
            _uow = uow;
        }

        public async Task<SeatPlanDto> GetSeatPlanAsync(Guid busScheduleId)
        {
            var seats = await _seatRepo.GetSeatsByScheduleIdAsync(busScheduleId);
            return new SeatPlanDto
            {
                BusScheduleId = busScheduleId,
                Seats = seats.Select(s => new SeatDto
                {
                    Id = s.Id,
                    Number = s.Number,
                    Row = s.Row,
                    Status = s.Status
                }).ToList()
            };
        }

        public async Task<BookSeatResultDto> BookSeatAsync(BookSeatInputDto input)
        {
            // Basic validation and transaction:
            var schedule = await _scheduleRepo.GetByIdAsync(input.BusScheduleId);
            if (schedule == null) return new BookSeatResultDto { Success = false, Message = "Schedule not found." };

            // Start transaction via UnitOfWork
            using var trx = await _uow.BeginTransactionAsync();
            try
            {
                // Check each seat is available
                var seats = await _seatRepo.GetByIdsAsync(input.SeatIds);
                if (seats.Count() != input.SeatIds.Count)
                    return new BookSeatResultDto { Success = false, Message = "One or more seats not found." };

                if (seats.Any(s => s.Status != SeatStatus.Available))
                    return new BookSeatResultDto { Success = false, Message = "One or more seats already booked." };

                // Create passenger
                var passenger = new Passenger { Id = Guid.NewGuid(), Name = input.PassengerName, MobileNumber = input.PassengerMobile };
                await _passengerRepo.AddAsync(passenger);

                // Create ticket
                var ticket = new Ticket
                {
                    Id = Guid.NewGuid(),
                    BusScheduleId = schedule.Id,
                    BookingTime = DateTime.UtcNow,
                    PassengerId = passenger.Id,
                    Passenger = passenger,
                    BoardingPoint = input.BoardingPoint,
                    DroppingPoint = input.DroppingPoint,
                    TotalAmount = schedule.Price * input.SeatIds.Count
                };
                await _ticketRepo.AddAsync(ticket);

                // Update seats and link to ticket
                foreach (var seat in seats)
                {
                    seat.Status = SeatStatus.Booked;
                    seat.TicketId = ticket.Id;
                    await _seatRepo.UpdateAsync(seat);
                }

                await _uow.SaveChangesAsync();
                await trx.CommitAsync();

                return new BookSeatResultDto { Success = true, TicketId = ticket.Id, Message = "Booked successfully." };
            }
            catch (Exception ex)
            {
                await trx.RollbackAsync();
                return new BookSeatResultDto { Success = false, Message = $"Booking failed: {ex.Message}" };
            }
        }
    }
}
