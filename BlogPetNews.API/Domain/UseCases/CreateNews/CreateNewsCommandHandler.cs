using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Domain.Users;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, CreateNewsCommandResponse>
{
    private readonly INewsRepository _newsRepository;
    private readonly IUserRepository _userRepository;

    public CreateNewsCommandHandler(INewsRepository newsRepository, IUserRepository userRepository)
    {
        _newsRepository = newsRepository;
        _userRepository = userRepository;
    }

    public async Task<CreateNewsCommandResponse> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByEmail(request.UserEmail);

        var news = new News.News()
        {
            Title = request.Title,
            Content = request.Content,
            UserId = user.Id
        };

        var addedNews = _newsRepository.Create(news);
        if (addedNews != null)
        {
            return new CreateNewsCommandResponse { News = addedNews, Success = true };
        }

        return new CreateNewsCommandResponse { News = null, Success = false };
    }
}
