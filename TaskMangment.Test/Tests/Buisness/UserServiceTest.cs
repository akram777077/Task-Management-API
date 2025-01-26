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
    }
}