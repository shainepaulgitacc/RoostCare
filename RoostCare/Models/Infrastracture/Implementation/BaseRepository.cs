
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using RoostCare.Models.Services;

namespace RoostCare.Models.Infrastracture.Implementation
{
    public class BaseRepository<T>:IBaseRepository<T>
        where T: class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _table;
        public BaseRepository(
            ApplicationDbContext db
            )
        {
            _db = db;
            _table = db.Set<T>();
        }

        public async Task Add(object obj)
        {
            await _table.AddAsync((T)obj);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(string Id)
        {
            var record = await GetOne(Id);
            if (record != null)
            {
                _table.Remove(record);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
           return  await _table.ToListAsync();
        }

        public async Task<T> GetOne(string Id)
        {
            if(int.TryParse(Id, out int ParsedId))
            {
                return await _table.FindAsync(ParsedId);
            }
            return await _table.FindAsync(Id);
            
        }

        public async Task Update(string Id, object obj)
        {
            var record = await GetOne(Id);
            if(record != null)
            {
                _table.Entry(record).CurrentValues.SetValues((T)obj);
                await _db.SaveChangesAsync();
            }
        }
    }
}
