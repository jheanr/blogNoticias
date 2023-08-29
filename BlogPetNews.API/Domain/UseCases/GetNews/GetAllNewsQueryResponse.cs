namespace BlogPetNews.API.Domain.UseCases.GetNews;

public class GetAllNewsQueryResponse
{
    public IEnumerable<News.News> News { get; set; }
}
