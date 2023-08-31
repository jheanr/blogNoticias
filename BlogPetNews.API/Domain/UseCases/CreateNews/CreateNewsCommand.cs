using BlogPetNews.API.Service.ViewModels.News;
using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommand : IRequest<CreateNewsCommandResponse>
{
  public  CreateNewsDto CreateNewsDto { get; set; }
  public string UserEmail { get; set; }
}
