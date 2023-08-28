using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Utils;

using MediatR;

namespace BlogPetNews.API.Domain.UseCases.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResponse>
    {
        private readonly IUserRepository _userService;
        private readonly TokenService _tokenService;

        public LoginUserCommandHandler(IUserRepository userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var token = Login(request.Email, request.Password);
            return new LoginUserCommandResponse { Token = token, Success = true };
        }

        private string Login(string email, string password)
        {
            var user = _userService.Login(email, password);
            if (user is not null)
            {
                var token = _tokenService.GenerateToken(user);
                return token;
            }
            return "Unauthorized Access";
        }
    }
}
