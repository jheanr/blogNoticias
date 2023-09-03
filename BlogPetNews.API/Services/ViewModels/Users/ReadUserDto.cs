using BlogPetNews.API.Domain.Enums;

namespace BlogPetNews.API.Service.ViewModels.Users
{
    public class ReadUserDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RolesUser Role { get; set; }
    }
}
