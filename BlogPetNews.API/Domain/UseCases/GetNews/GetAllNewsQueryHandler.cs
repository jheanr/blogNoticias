using BlogPetNews.API.Service.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.GetNews;

public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, GetAllNewsQueryResponse>
{
    private readonly INewsService _newsService;

    public GetAllNewsQueryHandler(INewsService newsService)
    {
        _newsService = newsService;
    }

    public async Task<GetAllNewsQueryResponse> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        var news = new GetAllNewsQueryResponse
        {
            News = await _newsService.GetAll()
        };

        return news;
    }
}
