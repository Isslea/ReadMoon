using ReadMoon.Data.Base;
using ReadMoon.Models;

namespace ReadMoon.Data.Services;

public interface IPublisherService : IEntityBaseRepository<Publisher>
{
    Task<Publisher> GetPublisherByIdAsync(int id);
}
    