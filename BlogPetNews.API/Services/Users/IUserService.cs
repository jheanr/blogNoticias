using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.Users
{
    public interface IUserService
    {
        IEnumerable<ReadUserDto> GetAll(int page, int take);
        ReadUserDto GetById(Guid id);
        ReadUserDto GetByEmail(string email);
        ReadUserDto Create(CreateUserDto user);
        ReadUserDto Update(UpdateUserDto user, Guid id);
        void Delete(Guid id);
        ReadUserDto Login(string email, string password);
    }
}
