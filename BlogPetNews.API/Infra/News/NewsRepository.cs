using AutoMapper;
using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.News
{
    public class NewsRepository : BaseRepository<Domain.News.News>, INewsRepository
    {
        public NewsRepository(BlogPetNewsDbContext context, IMapper map) : base(context)
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

        public IEnumerable<Domain.News.News> GetAll()
        {
            return _dbSet.Include(news => news.User).ToList();
        }

        public Domain.News.News GetById(Guid id)
        {
            return _dbSet.Where(news => news.Id.Equals(id)).Include(x => x.User).FirstOrDefault();
        }

        public Domain.News.News Update(Domain.News.News news)
        {
            _context.Update(news);
            _context.SaveChanges();

            return news;
        }
    }
}
