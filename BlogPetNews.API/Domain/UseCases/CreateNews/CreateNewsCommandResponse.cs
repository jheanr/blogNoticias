namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommandResponse
{
    public News.News News { get; set; }
    public bool Success { get; set; }
}
