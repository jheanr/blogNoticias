using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogPetNews.API.Infra.News
{
    public class NewsRepository : BaseRepository<Domain.News.News>, INewsRepository
    {
       
        public NewsRepository(BlogPetNewsDbContext context) : base(context)
        {
        }

        public Domain.News.News Create(Domain.News.News news)
        {
            _dbSet.Add(news);
            _context.SaveChanges();

            return news;
        }

        public void Delete(Guid id)
        {
             Domain.News.News news = GetById(id);
            _context.Remove(news);
            _context.SaveChanges();
        }

        public IEnumerable<Domain.News.News> GetAll(string? search, int page = 1, int take = 10)
        {
            int skip = (page - 1) * take;

            return _dbSet.Where(news => news.Title.Contains(search ?? "")).Skip(skip).Take(take).ToList();

        }

        public Domain.News.News GetById(Guid id)
        {
          return _dbSet.Where(news => news.Id == id).First();
        }

        public Domain.News.News Update(Domain.News.News news)
        {
            _context.Update(news);
            _context.SaveChanges();
            return news;
        }
    }
}
