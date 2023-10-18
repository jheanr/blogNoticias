using BlogPetNews.API.Domain.Enums;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using Bogus;

namespace BlogPetNews.Tests.Common.Users
{
    public static class UserTestFixture
    {
        public static Faker<User> UserFaker { get; } = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.PickRandom<RolesUser>())
            .RuleFor(u => u.DateCreated, f => f.Date.Recent(0));

        public static Faker<CreateUserDto> CreateUserDtoFaker { get; } = new Faker<CreateUserDto>()
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.PickRandom<RolesUser>());

        public static Faker<ReadUserDto> ReadUserDtoFaker { get; } = new Faker<ReadUserDto>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Role, f => f.PickRandom<RolesUser>())
            .RuleFor(u => u.DateCreated, f => f.Date.Recent(0));

        public static Faker<UpdateUserDto> UpdateUserDtoFaker { get; } = new Faker<UpdateUserDto>()
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.PickRandom<RolesUser>());
    }
}
