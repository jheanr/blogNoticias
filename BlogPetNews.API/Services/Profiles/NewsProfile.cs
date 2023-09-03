using AutoMapper;
using BlogPetNews.API.Domain.News;
using BlogPetNews.API.Service.ViewModels.News;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.Profiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<CreateNewsDto, Notice>();
            CreateMap<Notice, ReadNewsDto>().ForMember(news => news.Author, opts => opts.MapFrom(user => new ReadUserDto{ Id= user.User.Id, DateCreated = user.DateCreated, Name = user.User.Name, Email = user.User.Email, Role = user.User.Role }));
            CreateMap<UpdateNewsDto, Notice>();

        }
    }
}
