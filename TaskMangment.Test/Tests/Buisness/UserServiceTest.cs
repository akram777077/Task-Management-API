using Xunit;
using Moq;
using TaskMangment.Buisness.Services.SUser;
using TaskMangment.Data.Repositories.RUser;
using TaskMangment.Buisness.Models.Users;
using TaskMangment.Buisness.Mapping;
using TaskMangment.Data.Entities;

namespace TaskMangment.Test.Tests.Buisness
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AuthorizeAsync_ValidCredentials_ReturnsAuthorizeUserModel()
        {
            // Arrange
            var loginModel = new LoginModel("akram", "dris");
            var entity = loginModel.ToEntity();
            var result = new AuthorizeUserEntity { Username = "akram", Role = "admin" };
            _repositoryMock.Setup(r => r.AuthorizeUserAsync(It.IsAny<LoginEntity>())).ReturnsAsync(result);

            // Act
            var authorizeUserModel = await _userService.AuthorizeAsync(loginModel);

            // Assert
            Assert.NotNull(authorizeUserModel);
            Assert.Equal("akram", authorizeUserModel?.Username);
            Assert.Equal("admin", authorizeUserModel?.Role);
        }

        [Fact]
        public async Task AuthorizeAsync_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var loginModel = new LoginModel("akram", "dris");
            var entity = loginModel.ToEntity();
            _repositoryMock.Setup(r => r.AuthorizeUserAsync(entity)).ReturnsAsync((AuthorizeUserEntity?)null);

            // Act
            var authorizeUserModel = await _userService.AuthorizeAsync(loginModel);

            // Assert
            Assert.Null(authorizeUserModel);
        }
        [Fact]
        public async Task DeleteAsync_validUserName_ReturnTrue()
        {
            // Arrange
            var username = "virtualUser";
            var virtualUser = new UserModel(username,"1234","admin");
            _repositoryMock.Setup(r => r.DeleteUserAsync(username)).ReturnsAsync(true);

            // Act
            var result = await _userService.DeleteAsync(username);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.DeleteUserAsync(username), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_invalidUserName_ReturnFalse()
        {
            // Arrange
            var username = "wrongUsername";
            var existingUsername = "existingUser";
            _repositoryMock.Setup(r => r.DeleteUserAsync(existingUsername)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.DeleteUserAsync(username)).ReturnsAsync(false);

            // Act
            var result = await _userService.DeleteAsync(username);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.DeleteUserAsync(username), Times.Once);
        }
        [Fact]
        public async Task CreateAsync_ValidUser_ReturnsAuthorizeUserModel()
        {
            // Arrange
            var user = new UserModel("validUsername", "validPassword", "admin");
            var entity = user.ToEntity();
            _repositoryMock.Setup(r => r.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(true);

            // Act
            var authorizeUserModel = await _userService.CreateAsync(user);

            // Assert
            Assert.True(authorizeUserModel);
        }
        [Fact]
        public async Task UpdateAsync_ValidUser_ReturnsTrue()
        {
            // Arrange
            var user = new UserModel("validUsername", "validPassword", "admin");
            var entity = user.ToEntity();
            _repositoryMock.Setup(r => r.UpdateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateAsync(user);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.UpdateUserAsync(It.IsAny<UserEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidUser_ReturnsFalse()
        {
            // Arrange
            var user = new UserModel("invalidUsername", "invalidPassword", "invalidRole");
            var entity = user.ToEntity();
            _repositoryMock.Setup(r => r.UpdateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(false);

            // Act
            var result = await _userService.UpdateAsync(user);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(r => r.UpdateUserAsync(It.IsAny<UserEntity>()), Times.Once);
        }
    }
}