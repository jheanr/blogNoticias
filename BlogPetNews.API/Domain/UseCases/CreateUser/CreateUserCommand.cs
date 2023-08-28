using BlogPetNews.API.Domain.Users;

using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public User User { get; set; }
    }
}
