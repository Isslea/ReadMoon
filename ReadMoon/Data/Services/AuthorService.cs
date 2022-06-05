using ReadMoon.Data.Base;
using ReadMoon.Models;
using Microsoft.EntityFrameworkCore;

namespace ReadMoon.Data.Services;

public class AuthorService : EntityBaseRepository<Author>, IAuthorService
{
    private readonly AppDbContext _db;
    public AuthorService(AppDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        var authorDetails = await _db.Authors
            .Include(a => a.Books)
            .ThenInclude(a => a.Author)
            .FirstOrDefaultAsync(n => n.Id == id);
        return authorDetails;
    }
}