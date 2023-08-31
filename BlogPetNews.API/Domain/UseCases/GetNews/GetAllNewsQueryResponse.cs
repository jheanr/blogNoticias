using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Domain.UseCases.GetNews;

public class GetAllNewsQueryResponse
{
    public IEnumerable<ReadNewsDto> News { get; set; }
}
