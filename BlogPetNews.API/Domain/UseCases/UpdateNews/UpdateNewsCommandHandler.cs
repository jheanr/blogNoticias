using BlogPetNews.API.Service.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.UpdateNews
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, UpdateNewsCommandResponse>
    {
        private readonly INewsService _newsService;

        public UpdateNewsCommandHandler(INewsService newsService)
        {
            _newsService = newsService;
    
        }

        public async Task<UpdateNewsCommandResponse> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {


            var updateNews = await _newsService.Update(request.Id, request.UpdateNewsDto);

            if (updateNews is not null)
            {
                return new UpdateNewsCommandResponse { News = updateNews, Success = true };
            }

            return new UpdateNewsCommandResponse { News = null, Success = false };
        }
    }
}
