using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Utils;

using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository _userService;
        private readonly TokenService _tokenService;

        public CreateUserCommandHandler(IUserRepository userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = Create(request.User);
            if (user is not null)
                return new CreateUserCommandResponse { Success = true, User = user };
            else
                return new CreateUserCommandResponse { Success = false, User = null };
        }

        private User Create(User user)
        {
            var newUser = _userService.Create(user);
            if (newUser is not null)
                return newUser;
            else
                return null;
        }
    }
}
