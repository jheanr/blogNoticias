using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Infra.Utils;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService, TokenService tokenService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = Create(request.User);
            if (user is not null)
                return new CreateUserCommandResponse { Success = true, User = user };
            else
                return new CreateUserCommandResponse { Success = false, User = null };
        }

        private ReadUserDto Create(CreateUserDto user)
        {
            var newUser = _userService.Create(user);
            if (newUser is not null)
                return newUser;
            else
                return null;
        }
    }
}
