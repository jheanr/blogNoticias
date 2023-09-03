using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.EntityFrameworkCore;

namespace BlogPetNews.API.Infra.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ICryptography _Cryptography;
        public UserRepository(BlogPetNewsDbContext context) : base(context)
        {
            _Cryptography = new Cryptography();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbSet.Where(news => news.Email.Equals(email)).FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = await GetByEmail(email);

            if (user == null)
            {
                return null;
            }
            else
            {
                if (_Cryptography.Compares(password, user.Password))
                {
                    return user;

                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<User> CreateUser(User user)
        {
            user.Password = _Cryptography.Encodes(user.Password);
            await _context.Users.AddAsync(user);
            await SaveChanges();

            return user;
        }

    }
}
