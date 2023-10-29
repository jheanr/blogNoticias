using AutoMapper;
using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Service.News;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.Tests.Common.News;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BlogPetNews.Unit.Tests.Services.News
{
    public class NewsServiceTest
    {
        private readonly IMapper _mapper;
        private readonly NewsService _newsService;
        private readonly INewsRepository _newsRepository;

        public NewsServiceTest()
        {
            _mapper = Substitute.For<IMapper>();
            _newsRepository = Substitute.For<INewsRepository>();
            _newsService = new NewsService(_newsRepository, _mapper);
        }

        [Fact]
        public void Create_News_ShouldReturnSuccess()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var news = NewsTestFixture.NewsFaker.Generate();
            var newsDto = NewsTestFixture.CreateNewsDtoFaker.Generate();
            var readNewsDto = NewsTestFixture.ReadNewsDtoFaker.Generate();

            _mapper.Map<API.Domain.News.News>(newsDto).Returns(news);
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
        public void Create_News_ReturnsNull_WhenMapperReturnsNull()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var newsDto = NewsTestFixture.CreateNewsDtoFaker.Generate();

            _mapper.Map<API.Domain.News.News>(newsDto).ReturnsNull();

            //Act
            var response = _newsService.Create(newsDto, userId);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public void Update_News_ShouldReturnSuccess()
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
        public void Update_News_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var newsId = Guid.NewGuid();
            var updateNewsDto = NewsTestFixture.UpdateNewsDtoFaker.Generate();
            _newsRepository.GetById(newsId).ReturnsNull();

            // Act
            var response = _newsService.Update(newsId, updateNewsDto);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void Delete_News_ShouldReturnSuccess()
        {
            //Arrange
            var newsId = Guid.NewGuid();

            //Act
            _newsService.Delete(newsId);

            //Assert
            _newsRepository.Received(1).Delete(newsId);
        }

        [Fact]
        public void GetAll_News_ShouldReturnSuccess()
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
        public void GetAll_News_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            _newsRepository.GetAll().ReturnsNull();

            // Act
            var response = _newsService.GetAll();

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void GetById_News_ShouldReturnSuccess()
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

        [Fact]
        public void GetById_News_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var newsId = Guid.NewGuid();
            _newsRepository.GetById(newsId).ReturnsNull();

            // Act
            var response = _newsService.GetById(newsId);

            // Assert
            Assert.Null(response);
        }
    }
}