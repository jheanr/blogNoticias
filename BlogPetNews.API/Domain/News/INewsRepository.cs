using BlogPetNews.API.Infra.Utils;

namespace BlogPetNews.API.Domain.News
{
    public interface INewsRepository : IBaseRepository<Notice>
    {
        Task<IEnumerable<Notice>> GetAll();

    }
}
