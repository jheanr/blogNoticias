using BlogPetNews.API.Domain.UseCases.CreateNews;
using BlogPetNews.API.Domain.UseCases.GetNews;
using BlogPetNews.API.Domain.UseCases.UpdateNews;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.ViewModels.News;
using FluentValidation;
using MediatR;
using System.Security.Claims;

namespace BlogPetNews.API.Presentation.News;

public static class NewsModule
{
    public static void AddNewsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/news",  async (IMediator mediator) =>
        {
            var query = new GetAllNewsQuery();
            var result = await mediator.Send(query);

            return Results.Ok(result.News);
        }).AllowAnonymous();

        app.MapGet("/news/{id}", (INewsService newsService, Guid id) =>
        {
            var news = newsService.GetById(id);

            return news is not null
                ? Results.Ok(news) : Results.NotFound("News not found.");

        }).AllowAnonymous();

        app.MapPost("/news", async (IMediator mediator, IValidator<CreateNewsDto> validator, CreateNewsDto newsDto, ClaimsPrincipal user) =>
        {
            var validationResult = await validator.ValidateAsync(newsDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var createNewsCommand = new CreateNewsCommand
            {
                CreateNewsDto = newsDto,
                UserEmail = user.FindFirstValue(ClaimTypes.Email)
            };

            var result = mediator.Send(createNewsCommand);

            return Results.Ok(result.Result);
        }).RequireAuthorization();

        app.MapPut("/news/{id}", (IMediator mediator, INewsService newsService, UpdateNewsDto updatedNews, Guid id) =>
        {

            var UpdateCommand = new UpdateNewsCommand { UpdateNewsDto = updatedNews, Id = id };
            var update = mediator.Send(UpdateCommand);

            if (update.Result.News is null)
                return Results.NotFound("News not found.");

            return Results.Ok(update.Result);

        }).RequireAuthorization();

        app.MapDelete("/news/{id}", (INewsService newsService, Guid id) =>
        {
            var news = newsService.GetById(id);

            if (news is null)
                return Results.NotFound("News not found.");

            newsService.Delete(id);

            return Results.Ok("News successfully removed.");
        }).RequireAuthorization("Admin");
    }
}
