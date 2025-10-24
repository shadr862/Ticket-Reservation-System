using System.Threading.Tasks;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Ticket_Reservation_System_API.Model;

namespace Ticket_Reservation_System_API.Repositories
{
    public class TicketRepository:ITicketRepository
    {
        private readonly AppDbContext _db;
        public TicketRepository(AppDbContext db) { _db = db; }

        public async Task AddAsync(Ticket ticket)
        {
            await _db.Tickets.AddAsync(ticket);

        }
    }
}
