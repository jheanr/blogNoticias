using BlogPetNews.API.Service.ViewModels.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.UpdateNews
{
    public class UpdateNewsCommand : IRequest<UpdateNewsCommandResponse>
    {
        public UpdateNewsDto UpdateNewsDto { get; set; }
        public Guid Id { get; set; }
    }
}
