using Microsoft.EntityFrameworkCore;
using Moq;
using TaskMangment.Data;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Repositories.RUser;
using TaskMangment.Test.Data;

namespace TaskMangment.Test.Tests.DataTests;
public class UserRepositoryTest
{
    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var repo = new UserRepository(db);
        var user = new UserEntity()
        {
            UserName = "akram",
            Password = "test",
            RoleName = "admin",
            Role = new RoleEntity() { Name = "admin" }
        };

        await repo.CreateUserAsync(user);
        var result = await db.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
        Assert.NotNull(result);
    }
}
