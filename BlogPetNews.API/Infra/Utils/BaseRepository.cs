using BlogPetNews.API.Domain.Utils;
using BlogPetNews.API.Infra.Contexts;

using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.Utils
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : ClassBase
    {
        protected readonly BlogPetNewsDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(BlogPetNewsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _context.AddAsync(entity);
            await SaveChanges();

            return entity;
        }

        public async Task Delete(Guid id)
        {
            T entity = await GetById(id);
            _context.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll(int page, int take)
        {
            int skip = (page - 1) * take;
            var entities = await _dbSet.Skip(skip).Take(take).ToListAsync();

            return entities;
        }

        public async Task<T> GetById(Guid id)
        {
            var entity = await _dbSet.Where(news => news.Id == id).FirstOrDefaultAsync();
            return entity;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Update(entity);
            await SaveChanges();

            return entity;
        }
    }
}
