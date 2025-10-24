using Microsoft.EntityFrameworkCore.Storage;
using Ticket_Reservation_System_API.Data;

namespace Ticket_Reservation_System_API.Interfaces
{
    public interface IUnitOfWork
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;
        public Task<IDbContextTransaction> BeginTransactionAsync() => _db.Database.BeginTransactionAsync();
        public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
