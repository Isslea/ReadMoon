using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface ICategoryService : IEntityBaseRepository<Category>
{
    Task<Category> GetCategoryByIdAsync(int id);
}