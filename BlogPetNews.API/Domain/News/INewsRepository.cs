namespace BlogPetNews.API.Domain.News
{
    public interface INewsRepository
    {
        IEnumerable<News> GetAll(string? search, int page, int take);
        News GetById(Guid id);
        News Create(News news);
        News Update(News news);
        void Delete(Guid id);
    }
}
