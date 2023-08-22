using BlogPetNews.API.Domain.Users;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommandResponse
    {
        public User User { get; set; }
        public bool Success { get; set; }
    }
}
