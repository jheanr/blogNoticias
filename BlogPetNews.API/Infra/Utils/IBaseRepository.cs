using BlogPetNews.API.Domain.Utils;

namespace BlogPetNews.API.Infra.Utils
{
    public interface IBaseRepository<T> where T : ClassBase
    {
        Task<IEnumerable<T>> GetAll(int page, int take);
        Task<T> GetById(Guid id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid id);
        Task SaveChanges();
    }
}
