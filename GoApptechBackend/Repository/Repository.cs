using GoApptechBackend.Data;
using GoApptechBackend.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace GoApptechBackend.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext context;
        public DbSet<T> dbSet { get; set; }

        public Repository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> temp = dbSet;
            if (filter != null)
            {
                temp = temp.Where(filter);
            }

            // Använd ToListAsync direkt på IQueryable
            return await temp.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> temp = dbSet;
            if (!tracked == true)
            {
                temp = temp.AsNoTracking();
            }
            if (filter != null)
            {
                temp = temp.Where(filter);
            }
            return await temp.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await context.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
