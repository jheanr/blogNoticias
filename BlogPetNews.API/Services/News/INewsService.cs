using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Service.News
{
    public interface INewsService
    {
        Task<IEnumerable<ReadNewsDto>> GetAll();
        Task<ReadNewsDto> GetById(Guid id);
        Task<ReadNewsDto> Create(CreateNewsDto news, Guid userId);
        Task<ReadNewsDto> Update(Guid Id, UpdateNewsDto news);
        Task Delete(Guid id);
    }
}
