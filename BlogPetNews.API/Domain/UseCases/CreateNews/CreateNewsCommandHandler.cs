using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.Users;
using MediatR;


namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, CreateNewsCommandResponse>
{
    private readonly INewsService _newsService;
    private readonly IUserService _userService;

    public CreateNewsCommandHandler(INewsService newsService, IUserService userService)
    {
        _newsService = newsService;
        _userService = userService;
    }

    public async Task<CreateNewsCommandResponse> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
      

        var user = await _userService.GetByEmail(request.UserEmail);

        var addedNews = await _newsService.Create(request.CreateNewsDto, user.Id);

        if (addedNews != null)
        {
            return new CreateNewsCommandResponse { News = addedNews, Success = true };
        }

        return new CreateNewsCommandResponse { News = null, Success = false };
    }
}
