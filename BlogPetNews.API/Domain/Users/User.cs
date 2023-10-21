using BlogPetNews.API.Domain.Enums;
using BlogPetNews.API.Domain.Utils;

namespace BlogPetNews.API.Domain.Users
{
    public class User : ClassBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RolesUser Role { get; set; }
        public virtual ICollection<News.News> News { get; set; }

        public User() { }

        public User(string name, string email, string password, RolesUser role) : base()
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }

        public User(string name, string email, RolesUser role)
        {
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
