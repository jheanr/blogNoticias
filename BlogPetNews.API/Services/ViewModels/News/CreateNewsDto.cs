
using FluentValidation;

namespace BlogPetNews.API.Service.ViewModels.News
{
    public class CreateNewsDto
    {
      
        public string Title { get; set; }
        public string Content { get; set; }

        public class CreateNewsDtoValidator : AbstractValidator<CreateNewsDto>
        {
            public CreateNewsDtoValidator()
            {
                RuleFor(x => x.Title).NotNull().MinimumLength(2).MaximumLength(100);
                RuleFor(x => x.Content).NotNull().MinimumLength(2);
            }
        }
    }
}
