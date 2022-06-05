using ReadMoon.Models;
using ReadMoon.Data.Base;

namespace ReadMoon.Data.Services;

public interface IAuthorService: IEntityBaseRepository<Author>
{
    Task<Author> GetAuthorByIdAsync(int id);
}