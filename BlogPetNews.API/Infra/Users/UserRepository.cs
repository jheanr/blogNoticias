using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Contexts;
using BlogPetNews.API.Infra.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogPetNews.API.Infra.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ICryptography _Cryptography;
        public UserRepository(BlogPetNewsDbContext context) : base(context)
        {
            _Cryptography = new Cryptography();
        }

        public User Create(User user)
        {
            user.Password = _Cryptography.Encodes(user.Password);
            _dbSet.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(Guid id)
        {
            User user = GetById(id);
            _context.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll(int page, int take)
        {
            int skip = (page - 1) * take;

            return _dbSet.Skip(skip).Take(take).ToList();
        }

        public User GetById(Guid id)
        {
            return _dbSet.Where(news => news.Id == id).First();
        }

        public User? Login(string email, string password)
        {
           var user = _dbSet.Where(user => user.Email == email).FirstOrDefault();

            if (user == null)
            {
                return null;

            } else
            {
                if(_Cryptography.Compares(password, user.Password))
                {
                    return user;

                } else
                {
                    return null;
                }
            }
        }

        public User Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
