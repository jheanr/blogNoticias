using BlogPetNews.API.Domain.UseCases.CreateUser;
using BlogPetNews.API.Domain.UseCases.LoginUser;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.API.Service.ViewModels.Users;
using FluentValidation;
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

            if (login.Result.Token == "Unauthorized Access")
                return Results.Unauthorized();
            
            return login is not null ? Results.Ok(login) : Results.BadRequest();
        }).AllowAnonymous();

        app.MapPost("/create", async (IMediator mediator, IValidator<CreateUserDto> validator, CreateUserDto user) =>
        {

            var validationResult = await validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }


            var createCommand = new CreateUserCommand { User = user };
            var created = mediator.Send(createCommand);

            return Results.Ok(created);

        }).AllowAnonymous();
    }
}
