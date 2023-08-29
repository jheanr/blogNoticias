using BlogPetNews.API.Domain.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, CreateNewsCommandResponse>
{
    private readonly INewsRepository _newsRepository;

    public CreateNewsCommandHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<CreateNewsCommandResponse> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = new News.News()
        {
            Title = request.Title,
            Content = request.Content,
            UserId = request.UserId
        };

        var addedNews = _newsRepository.Create(news);
        if (addedNews != null)
        {
            return new CreateNewsCommandResponse { News = addedNews, Success = true };
        }

        return new CreateNewsCommandResponse { News = null, Success = false };
    }
}
