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

        public async Task<ReadNewsDto> Create(CreateNewsDto newsDto, Guid userId)
        {
            Notice news = _mapper.Map<Notice>(newsDto);
            news.UserId = userId;

            return _mapper.Map<ReadNewsDto>(await _newsRepository.Create(news));
        }

        public async Task Delete(Guid id)
        {
            await _newsRepository.Delete(id);
        }

        public async Task<IEnumerable<ReadNewsDto>> GetAll()
        {
            IEnumerable<Notice> news = await _newsRepository.GetAll();

            if (news is not null)
            {
                IEnumerable<ReadNewsDto> newsDto = _mapper.Map<IEnumerable<ReadNewsDto>>(news);

                return newsDto;
            }

            return null;
               
        }

        public async Task<ReadNewsDto> GetById(Guid id)
        {
           Notice news = await _newsRepository.GetById(id);

            if (news is not null)
            {
                ReadNewsDto newsDto = _mapper.Map<ReadNewsDto>(news);

                return newsDto;
            }

            return null;
        }

        public async Task<ReadNewsDto> Update(Guid id, UpdateNewsDto newsDto)
        {
            Notice news = await _newsRepository.GetById(id);

            if (news is null)
                return null;

           Notice newsUpdate = _mapper.Map(newsDto, news);

            return _mapper.Map<ReadNewsDto>(_newsRepository.Update(newsUpdate));
        }
    }
}
