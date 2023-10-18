using AutoMapper;
using BlogPetNews.API.Domain.Users;
using BlogPetNews.API.Service.Users;
using BlogPetNews.API.Service.ViewModels.Users;
using BlogPetNews.Tests.Common.Users;
using NSubstitute;

namespace BlogPetNews.Unit.Tests.Post
{
    public class PostUserTest
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly IUserRepository _userRepository;

        public PostUserTest()
        {
            _mapper = Substitute.For<IMapper>();
            _userRepository = Substitute.For<IUserRepository>();
            _userService = new UserService(_userRepository, _mapper);
        }

        #region Services Tests
        [Fact]
        public void Should_Login_User_Successfully()
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
        public void Should_Create_User_Successfully()
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
        public void Should_Update_User_Successfully()
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
        public void Should_Delete_User_Successfully()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            _userService.Delete(userId);

            // Assert
            _userRepository.Received(1).Delete(userId);
        }

        [Fact]
        public void Should_Get_All_Users_Successfully()
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
        public void Should_Get_User_By_Email_Successfully()
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
        public void Should_Get_User_By_Id_Successfully()
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
        #endregion
    }
}