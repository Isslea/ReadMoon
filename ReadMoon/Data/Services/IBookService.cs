using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface IBookService : IEntityBaseRepository<Book>
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<NewDropDownVM> GetNewBookDropdownsValues();
    Task AddNewBookAsync(NewBookVM data);
    Task UpdateBookAsync(NewBookVM data);
    Task DeleteNewBookAsync(int id);
}