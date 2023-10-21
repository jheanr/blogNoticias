using AutoMapper;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, ReadUserDto>();
            CreateMap<ReadUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
