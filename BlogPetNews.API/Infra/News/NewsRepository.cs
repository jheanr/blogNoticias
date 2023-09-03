using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.News
{
    public class NewsRepository : BaseRepository<Notice>, INewsRepository
    {
        public NewsRepository(BlogPetNewsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notice>> GetAll()
        {
            var entities = await _dbSet.ToListAsync();

            return entities;
        }


    }
}
