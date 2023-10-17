using AutoMapper;
using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.News;
using NSubstitute;
using Xunit;

namespace BlogPetNews.Unit.Tests.Post
{
    public class PostNewsTest
    {
        private readonly IMapper _mapper;
        private readonly NewsService _newsService;
        private readonly INewsRepository _newsRepository;

        public PostNewsTest()
        {
            _mapper = Substitute.For<IMapper>();
            _newsRepository = Substitute.For<INewsRepository>();
            _newsService = new NewsService(_newsRepository, _mapper);
        }

        #region Services Tests
        [Fact]
        public void Should_Create_News_Successfully()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var news = NewsTestFixture.NewsFaker.Generate();
            var newsDto = NewsTestFixture.CreateNewsDtoFaker.Generate();
            var readNewsDto = NewsTestFixture.ReadNewsDtoFaker.Generate();

            _mapper.Map<News>(newsDto).Returns(news);
            news.UserId = userId;
            _newsRepository.Create(news).Returns(news);
            _mapper.Map<ReadNewsDto>(news).Returns(readNewsDto);

            //Act
            var response = _newsService.Create(newsDto, userId);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(readNewsDto, response);
        }
        #endregion
    }
}