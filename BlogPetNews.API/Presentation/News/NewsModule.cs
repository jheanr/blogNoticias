using BlogPetNews.API.Domain.News;

namespace BlogPetNews.API.Presentation.News;

public static class NewsModule
{
    public static void AddNewsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/news", (INewsRepository newsRepository) => newsRepository.GetAll());

        app.MapGet("/news/{id}", (INewsRepository newsRepository, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            return news is Domain.News.News result
                ? Results.Ok(result) : Results.NotFound("News not found.");
        });

        app.MapPost("/news", (INewsRepository newsRepository, Domain.News.News news) =>
        {
            var result = newsRepository.Create(news);

            return Results.Ok(result);
        });

        app.MapPut("/news/{id}", (INewsRepository newsRepository, Domain.News.News updatedNews, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            if (news is null)
                return Results.NotFound("News not found.");

            news.Title = updatedNews.Title;
            news.Content = updatedNews.Content;

            return Results.Ok(news);
        });

        app.MapDelete("/news/{id}", async (INewsRepository newsRepository, Guid id) =>
        {
            var news = newsRepository.GetById(id);

            if (news is null)
                return Results.NotFound("News not found.");

            newsRepository.Delete(id);

            return Results.Ok("News successfully removed.");
        });
    }
}
