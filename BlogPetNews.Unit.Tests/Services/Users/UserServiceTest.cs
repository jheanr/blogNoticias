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
        [Trait("Login", "Login user with success")]
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
        [Trait("Login", "Try login user with null return from repository")]
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
        [Trait("Create", "Create user with success")]
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
        [Trait("Create", "Try creating user with null return from mapper")]
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
        [Trait("Update", "Update user with success")]
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
        [Trait("Update", "Try updating user with null return from repository")]
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
        [Trait("Delete", "Delete user with success")]
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
        [Trait("Read", "Get all user with success")]
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
        [Trait("Read", "Try getting all user with null return from repository")]
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
        [Trait("Read", "Get user by email with success")]
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
        [Trait("Read", "Try getting user by email with null return from repository")]
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
        [Trait("Read", "Get user by id with success")]
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
        [Trait("Read", "Try getting user by id with null return from repository")]
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