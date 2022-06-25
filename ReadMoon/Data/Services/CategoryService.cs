using Microsoft.EntityFrameworkCore;
using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface ICategoryService : IEntityBaseRepository<Category>
{
    Task<Category> GetCategoryByIdAsync(int id);
}
public class CategoryService: EntityBaseRepository<Category>, ICategoryService
{
    private readonly AppDbContext _db;
    public CategoryService(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var categoryDetails = await _db.Categories
            .Include(b => b.Books)
            .ThenInclude(a => a.Author)
            .FirstOrDefaultAsync(n => n.Id == id);
        return categoryDetails;
    }

}
    