using MediatR;

namespace BlogPetNews.API.Domain.UseCases.CreateNews;

public class CreateNewsCommand : IRequest<CreateNewsCommandResponse>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
}
