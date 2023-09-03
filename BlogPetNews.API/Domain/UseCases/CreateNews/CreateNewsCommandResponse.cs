using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommandResponse
{
    public ReadNewsDto News { get; set; }
    public bool Success { get; set; }
}
