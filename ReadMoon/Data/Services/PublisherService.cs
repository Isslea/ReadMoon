using Microsoft.EntityFrameworkCore;
using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface IPublisherService : IEntityBaseRepository<Publisher>
{
    Task<Publisher> GetPublisherByIdAsync(int id);
}

public class PublisherService: EntityBaseRepository<Publisher>, IPublisherService
{
    private readonly AppDbContext _db;
    public PublisherService(AppDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<Publisher> GetPublisherByIdAsync(int id)
    {
        var bookDetails = await _db.Publishers
            .Include(b => b.Books)
            .ThenInclude(p => p.Author)
            .FirstOrDefaultAsync(n => n.Id == id);
        return bookDetails;
    }
    
}