using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Service.News
{
    public interface INewsService
    {
        IEnumerable<ReadNewsDto> GetAll();
        ReadNewsDto GetById(Guid id);
        ReadNewsDto Create(CreateNewsDto news, Guid userId);
        ReadNewsDto Update(Guid Id, UpdateNewsDto news);
        void Delete(Guid id);
    }
}
