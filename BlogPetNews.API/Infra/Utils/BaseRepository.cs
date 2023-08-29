using BlogPetNews.API.Domain.Utils;
using BlogPetNews.API.Infra.Contexts;

using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.Utils
{
    public class BaseRepository<T> where T : ClassBase
    {
        protected readonly BlogPetNewsDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(BlogPetNewsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    }
}
