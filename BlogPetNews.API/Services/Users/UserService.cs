using AutoMapper;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.ViewModels.Users;

namespace BlogPetNews.API.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public ReadUserDto Create(CreateUserDto userDto)
        {
            var user = _userRepository.Create(_mapper.Map<User>(userDto));

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public void Delete(Guid id)
        {
           _userRepository.Delete(id);
        }

        public IEnumerable<ReadUserDto> GetAll(int page, int take)
        {
            var users = _userRepository.GetAll(page, take);

            if (users is null)
                return null;

            return _mapper.Map<IEnumerable<ReadUserDto>>(users);
        }

        public ReadUserDto GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public ReadUserDto GetById(Guid id)
        {
            var user = _userRepository.GetById(id);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public ReadUserDto Login(string email, string password)
        {
            var user = _userRepository.Login(email, password);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public ReadUserDto Update(UpdateUserDto userDto, Guid id)
        {

            var UserId = _userRepository.GetById(id);

            var response = _userRepository.Update(_mapper.Map(userDto, UserId));

            if (response is null)
                return null;

            return _mapper.Map<ReadUserDto>(response);
        }
    }
}
