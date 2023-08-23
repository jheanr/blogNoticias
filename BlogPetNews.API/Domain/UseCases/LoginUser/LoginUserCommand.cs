using BlogPetNews.API.Domain.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
