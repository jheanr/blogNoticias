using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public CreateUserDto User { get; set; }
    }
}
