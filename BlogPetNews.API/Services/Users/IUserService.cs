using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.Users
{
    public interface IUserService 
    {
        Task<IEnumerable<ReadUserDto>> GetAll(int page, int take);
        Task<ReadUserDto> GetById(Guid id);
        Task<ReadUserDto> GetByEmail(string email);
        Task<ReadUserDto> Create(CreateUserDto user);
        Task<ReadUserDto> Update(UpdateUserDto user, Guid id);
        Task Delete(Guid id);
        Task<ReadUserDto> Login(string email, string password);
    }
}
