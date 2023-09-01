using AutoMapper;
using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Service.ViewModels.News;

namespace BlogPetNews.API.Service.News
{
    public class NewsService : INewsService
    {
        private readonly IMapper _mapper;
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;
        }

        public ReadNewsDto Create(CreateNewsDto newsDto, Guid userId)
        {
            Domain.News.News news = _mapper.Map<Domain.News.News>(newsDto);
            news.UserId = userId;

            return _mapper.Map<ReadNewsDto>(_newsRepository.Create(news));
        }

        public void Delete(Guid id)
        {
            _newsRepository.Delete(id);
        }

        public IEnumerable<ReadNewsDto> GetAll()
        {
            IEnumerable<Domain.News.News> news = _newsRepository.GetAll();

            if (news is not null)
            {
                IEnumerable<ReadNewsDto> newsDto = _mapper.Map<IEnumerable<ReadNewsDto>>(news);

                return newsDto;
            }

            return null;
               
        }

        public ReadNewsDto GetById(Guid id)
        {
           Domain.News.News news = _newsRepository.GetById(id);

            if (news is not null)
            {
                ReadNewsDto newsDto = _mapper.Map<ReadNewsDto>(news);

                return newsDto;
            }

            return null;
        }

        public ReadNewsDto Update(Guid id, UpdateNewsDto newsDto)
        {
            Domain.News.News news = _newsRepository.GetById(id);

            if (news is null)
                return null;

           Domain.News.News newsUpdate = _mapper.Map(newsDto, news);

            return _mapper.Map<ReadNewsDto>(_newsRepository.Update(newsUpdate));
        }
    }
}
