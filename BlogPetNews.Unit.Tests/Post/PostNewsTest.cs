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

        [Fact]
        public void Should_Update_News_Successfully()
        {
            //Arrange
            var newsId = Guid.NewGuid();
            var news = NewsTestFixture.NewsFaker.Generate();
            var newsDto = NewsTestFixture.UpdateNewsDtoFaker.Generate();
            var newsUpdate = NewsTestFixture.NewsFaker.Generate();
            var readNewsDto = NewsTestFixture.ReadNewsDtoFaker.Generate();

            _newsRepository.GetById(newsId).Returns(news);
            _mapper.Map(newsDto, news).Returns(newsUpdate);
            _newsRepository.Update(newsUpdate).Returns(newsUpdate);
            _mapper.Map<ReadNewsDto>(newsUpdate).Returns(readNewsDto);

            //Act
            var response = _newsService.Update(newsId, newsDto);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(readNewsDto, response);
        }

        [Fact]
        public void Should_Delete_News_Successfully()
        {
            //Arrange
            var newsId = Guid.NewGuid();

            //Act
            _newsService.Delete(newsId);

            //Assert
            _newsRepository.Received(1).Delete(newsId);
        }

        [Fact]
        public void Should_GetAll_News_Successfully()
        {
            // Arrange
            var newsList = NewsTestFixture.NewsFaker.Generate(3);
            var readNewsDtos = NewsTestFixture.ReadNewsDtoFaker.Generate(3);

            _newsRepository.GetAll().Returns(newsList);
            _mapper.Map<IEnumerable<ReadNewsDto>>(newsList).Returns(readNewsDtos);

            // Act
            var response = _newsService.GetAll();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(newsList.Count, response.Count());
            Assert.Equal(readNewsDtos, response);
        }

        [Fact]
        public void Should_GetId_News_Successfully()
        {
            // Arrange
            var newsId = Guid.NewGuid();
            var news = NewsTestFixture.NewsFaker.Generate();
            var readNewsDto = NewsTestFixture.ReadNewsDtoFaker.Generate();

            _newsRepository.GetById(newsId).Returns(news);
            _mapper.Map<ReadNewsDto>(news).Returns(readNewsDto);

            // Act
            var response = _newsService.GetById(newsId);

            // Assert
            Assert.Equal(readNewsDto, response);
        }
        #endregion
    }
}