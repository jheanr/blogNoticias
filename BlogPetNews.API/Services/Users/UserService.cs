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

        public async Task<ReadUserDto> Create(CreateUserDto userDto)
        {
            var user = await _userRepository.Create(_mapper.Map<User>(userDto));

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task Delete(Guid id)
        {
           await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<ReadUserDto>> GetAll(int page, int take)
        {
            var users = await _userRepository.GetAll(page, take);

            if (users is null)
                return null;

            return _mapper.Map<IEnumerable<ReadUserDto>>(users);
        }

        public async Task<ReadUserDto> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<ReadUserDto> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<ReadUserDto> Login(string email, string password)
        {
            var user = await _userRepository.Login(email, password);

            if (user is null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<ReadUserDto> Update(UpdateUserDto userDto, Guid id)
        {

            var UserId = await _userRepository.GetById(id);

            var response = await _userRepository.Update(_mapper.Map(userDto, UserId));

            if (response is null)
                return null;

            return _mapper.Map<ReadUserDto>(response);
        }
    }
}
