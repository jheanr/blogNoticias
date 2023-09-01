using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommandResponse
    {
        public ReadUserDto User { get; set; }
        public bool Success { get; set; }
    }
}
