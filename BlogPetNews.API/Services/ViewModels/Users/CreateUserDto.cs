using BlogPetNews.API.Domain.Enums;
using FluentValidation;

namespace BlogPetNews.API.Service.ViewModels.Users
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RolesUser Role { get; set; }

        public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
        {
            public CreateUserDtoValidator()
            {
                RuleFor(x => x.Name).NotNull().MinimumLength(2).MaximumLength(200);
                RuleFor(x => x.Email).NotNull().EmailAddress().MinimumLength(5);
                RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
                RuleFor(x => x.Role).NotNull().IsInEnum();
            }
        }

    }
}
