using BlogPetNews.API.Service.ViewModels.News;
using Bogus;

namespace BlogPetNews.Tests.Common.News
{
    public static class NewsTestFixture
    {
        public static Faker<API.Domain.News.News> NewsFaker { get; } = new Faker<API.Domain.News.News>()
            .RuleFor(n => n.Id, f => Guid.NewGuid())
            .RuleFor(n => n.Title, f => f.Lorem.Word())
            .RuleFor(n => n.Content, f => f.Lorem.Paragraph());

        public static Faker<CreateNewsDto> CreateNewsDtoFaker { get; } = new Faker<CreateNewsDto>()
            .RuleFor(n => n.Title, f => f.Lorem.Word())
            .RuleFor(n => n.Content, f => f.Lorem.Paragraph());

        public static Faker<ReadNewsDto> ReadNewsDtoFaker { get; } = new Faker<ReadNewsDto>()
            .RuleFor(n => n.Id, f => Guid.NewGuid())
            .RuleFor(n => n.Title, f => f.Lorem.Word())
            .RuleFor(n => n.Content, f => f.Lorem.Paragraph());
    }
}
