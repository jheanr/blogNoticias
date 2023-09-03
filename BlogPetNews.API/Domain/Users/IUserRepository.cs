using BlogPetNews.API.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Domain.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> Login(string email, string password);
        Task<User> CreateUser(User user);
       
    }
}
