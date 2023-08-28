using BlogPetNews.API.Domain.UseCases.CreateUser;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Domain.Users;
using MediatR;

namespace BlogPetNews.API.Presentation.Users;

public static class UserModule
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", (IMediator mediator, string email, string password) =>
        {
            var loginCommand = new LoginUserCommand { Email = email, Password = password };
            var login = mediator.Send(loginCommand);
            return login;
        }).AllowAnonymous();

        app.MapPost("/create", (IMediator mediator, User user) =>
        {
            var createCommand = new CreateUserCommand { user = user };
            var created = mediator.Send(createCommand);
            return created;
        }).AllowAnonymous();
    }
}
