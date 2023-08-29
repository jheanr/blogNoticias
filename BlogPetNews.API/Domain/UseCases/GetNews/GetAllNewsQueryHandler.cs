using BlogPetNews.API.Domain.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.GetNews;

public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, GetAllNewsQueryResponse>
{
    private readonly INewsRepository _newsRepository;

    public GetAllNewsQueryHandler(INewsRepository newsRepository)
    {
        _newsRepository = newsRepository;
    }

    public async Task<GetAllNewsQueryResponse> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        var news = new GetAllNewsQueryResponse
        {
            News = _newsRepository.GetAll()
        };

        return news;
    }
}
