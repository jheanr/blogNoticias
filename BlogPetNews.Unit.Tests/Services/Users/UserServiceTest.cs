using AutoMapper;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Tests.Common.Users;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BlogPetNews.Unit.Tests.Services.Users
{
    public class UserServiceTest
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

        public UserServiceTest()
        {
            _mapper = Substitute.For<IMapper>();
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository, _mapper);
        }

        [Fact]
        public void Login_User_ShouldReturnSuccess()
        {
            // Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var email = user.Email;
            var password = user.Password;
            var readUserDto = UserTestFixture.ReadUserDtoFaker.Generate();

            _userRepository.Login(email, password).Returns(user);
            _mapper.Map<ReadUserDto>(user).Returns(readUserDto);

            // Act
            var response = _userService.Login(email, password);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(readUserDto, response);
        }

        [Fact]
        public void Login_User_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var email = user.Email;
            var password = user.Password;
            _userRepository.Login(email, password).ReturnsNull();

            // Act
            var response = _userService.Login(email, password);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void Create_User_ShouldReturnSuccess()
        {
            // Arrange
            var createUserDto = UserTestFixture.CreateUserDtoFaker.Generate();
            var user = UserTestFixture.UserFaker.Generate();
            var readUserDto = UserTestFixture.ReadUserDtoFaker.Generate();

            _mapper.Map<User>(createUserDto).Returns(user);
            _userRepository.Create(user).Returns(user);
            _mapper.Map<ReadUserDto>(user).Returns(readUserDto);

            // Act
            var response = _userService.Create(createUserDto);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(readUserDto, response);
        }

        [Fact]
        public void Create_User_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var userDto = UserTestFixture.CreateUserDtoFaker.Generate();
            _userRepository.Create(Arg.Any<User>()).ReturnsNull();

            // Act
            var response = _userService.Create(userDto);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void Update_User_ShouldReturnSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserDto = UserTestFixture.UpdateUserDtoFaker.Generate();
            var user = UserTestFixture.UserFaker.Generate();
            var updatedUser = UserTestFixture.UserFaker.Generate();
            var readUserDto = UserTestFixture.ReadUserDtoFaker.Generate();

            _userRepository.GetById(userId).Returns(user);
            _mapper.Map(updateUserDto, user).Returns(updatedUser);
            _userRepository.Update(updatedUser).Returns(updatedUser);
            _mapper.Map<ReadUserDto>(updatedUser).Returns(readUserDto);

            // Act
            var response = _userService.Update(updateUserDto, userId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(readUserDto, response);
        }

        [Fact]
        public void Update_User_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserDto = UserTestFixture.UpdateUserDtoFaker.Generate();
            _userRepository.GetById(userId).ReturnsNull();

            // Act
            var response = _userService.Update(updateUserDto, userId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void Delete_User_ShouldReturnSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            _userService.Delete(userId);

            // Assert
            _userRepository.Received(1).Delete(userId);
        }

        [Fact]
        public void GetAll_Users_ShouldReturnSuccess()
        {
            // Arrange
            var page = 1;
            var take = 10;
            var userList = UserTestFixture.UserFaker.Generate(3);
            var readUserDtos = UserTestFixture.ReadUserDtoFaker.Generate(3);

            _userRepository.GetAll(page, take).Returns(userList);
            _mapper.Map<IEnumerable<ReadUserDto>>(userList).Returns(readUserDtos);

            // Act
            var response = _userService.GetAll(page, take);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(userList.Count, response.Count());
            Assert.Equal(readUserDtos, response);
        }

        [Fact]
        public void GetAll_Users_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            _userRepository.GetAll(Arg.Any<int>(), Arg.Any<int>()).ReturnsNull();

            // Act
            var response = _userService.GetAll(1, 10);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void GetByEmail_User_ShouldReturnSuccess()
        {
            // Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var email = user.Email;
            var readUserDto = UserTestFixture.ReadUserDtoFaker.Generate();

            _userRepository.GetByEmail(email).Returns(user);
            _mapper.Map<ReadUserDto>(user).Returns(readUserDto);

            // Act
            var response = _userService.GetByEmail(email);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(readUserDto, response);
        }

        [Fact]
        public void GetByEmail_User_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var user = UserTestFixture.UserFaker.Generate();
            var email = user.Email;
            _userRepository.GetByEmail(email).ReturnsNull();

            // Act
            var response = _userService.GetByEmail(email);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public void GetById_User_ShouldReturnSuccess()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = UserTestFixture.UserFaker.Generate();
            var readUserDto = UserTestFixture.ReadUserDtoFaker.Generate();

            _userRepository.GetById(userId).Returns(user);
            _mapper.Map<ReadUserDto>(user).Returns(readUserDto);

            // Act
            var response = _userService.GetById(userId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(readUserDto, response);
        }

        [Fact]
        public void GetById_User_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepository.GetById(userId).ReturnsNull();

            // Act
            var response = _userService.GetById(userId);

            // Assert
            Assert.Null(response);
        }
    }
}