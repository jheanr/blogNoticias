using BlogPetNews.API.Domain.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.LoginUser
{
    public class LoginUserCommandResponse 
    {
        public string Token { get; set; }
        public bool Success { get; set; }

    }
}
