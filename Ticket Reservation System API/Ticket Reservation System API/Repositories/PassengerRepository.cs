using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Repositories
{
    public class PassengerRepository:IPassengerRepository
    {
        private readonly AppDbContext _db;
        public PassengerRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Passenger passenger)
        {
            await _db.Passengers.AddAsync(passenger);
        }
    }
}
