using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ReadMoon.Data.Base;

public class EntityBaseRepository<T> where T: class, IEntityBase, new()
{
    private readonly AppDbContext _db;
    public EntityBaseRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();
    public async Task AddAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, T entity)
    {
        EntityEntry entityEntry = _db.Entry<T>(entity);
        entityEntry.State = EntityState.Modified;

        await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
        EntityEntry entityEntry = _db.Entry<T>(entity);
        entityEntry.State = EntityState.Deleted;

        await _db.SaveChangesAsync();
    }
}