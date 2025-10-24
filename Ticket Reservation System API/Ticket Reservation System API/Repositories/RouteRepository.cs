
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ticket_Reservation_System_API.Data;
using Ticket_Reservation_System_API.Interfaces;
using Route = Ticket_Reservation_System_API.Model.Route;


namespace Ticket_Reservation_System_API.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly AppDbContext _db;
        public RouteRepository(AppDbContext db) => _db = db;

        public async Task<Route?> GetByFromToAsync(string from, string to)
        {
            string normalizedFrom = from.Trim().ToLower();
            string normalizedTo = to.Trim().ToLower();

            Model.Route? route = await _db.Routes
                .AsNoTracking()
                .FirstOrDefaultAsync(r =>
                    r.From.ToLower() == normalizedFrom &&
                    r.To.ToLower() == normalizedTo);
            return route;
        }

        public async Task AddAsync(Route routee)
        {
            await _db.Routes.AddAsync(routee);
            await _db.SaveChangesAsync();
        }

       

    }

}
