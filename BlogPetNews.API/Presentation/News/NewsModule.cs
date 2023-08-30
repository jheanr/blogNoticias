using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.UseCases.CreateNews;
using BlogPetNews.API.Domain.UseCases.GetNews;
using MediatR;
using System.Security.Claims;

namespace BlogPetNews.API.Presentation.News;

public static class NewsModule
{
    public static void AddNewsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/news", (IMediator mediator) =>
        {
            var query = new GetAllNewsQuery();
            var result = mediator.Send(query);

            return result;
        }).AllowAnonymous();

        app.MapGet("/news/{id}", (INewsRepository newsRepository, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            return news is Domain.News.News result
                ? Results.Ok(result) : Results.NotFound("News not found.");
        }).AllowAnonymous();

        app.MapPost("/news", (IMediator mediator, CreateNewsCommand command, ClaimsPrincipal user) =>
        {
            var createNewsCommand = new CreateNewsCommand
            {
                Title = command.Title,
                Content = command.Content,
                UserEmail = user.FindFirstValue(ClaimTypes.Email)
            };

            var result = mediator.Send(createNewsCommand);

            return Results.Ok(result);
        }).RequireAuthorization();

        app.MapPut("/news/{id}", (INewsRepository newsRepository, Domain.News.News updatedNews, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            if (news is null)
                return Results.NotFound("News not found.");

            news.Title = updatedNews.Title;
            news.Content = updatedNews.Content;

            newsRepository.Update(news);

            return Results.Ok(news);
        }).RequireAuthorization();

        app.MapDelete("/news/{id}", async (INewsRepository newsRepository, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            if (news is null)
                return Results.NotFound("News not found.");

            newsRepository.Delete(id);

            return Results.Ok("News successfully removed.");
        }).RequireAuthorization("Admin");
    }
}
