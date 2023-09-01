using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Domain.UseCases.UpdateNews
{
    public class UpdateNewsCommandResponse
    {
        public ReadNewsDto News { get; set; }
        public bool Success { get; set; }
    }
}
