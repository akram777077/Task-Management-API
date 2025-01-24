using Microsoft.EntityFrameworkCore;
using Moq;
using TaskMangment.Data;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Repositories.RUser;
using TaskMangment.Test.Data;
using TaskMangment.Test.Extensions;

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
    [Fact]
    public async Task DeleteUserAsync_validUser_ShouldDeleteUser()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var repo = new UserRepository(db);
        bool deleteResult = await repo.DeleteUserAsync("akram");
        var result = await db.Users.FirstOrDefaultAsync(x => x.UserName == "akram");
        Assert.True(deleteResult);
        Assert.Null(result);
    }
    [Fact]
    public async Task DeleteUserAsync_invalidUser_ShouldNotDeleteUser()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var repo = new UserRepository(db);
        var deleteResult = await repo.DeleteUserAsync("aa");
        var result = await db.Users.FirstOrDefaultAsync(x => x.UserName == "aa");
        Assert.False(deleteResult);
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateUserAsync_validUser_ReturnTrue_ShouldUpdateUser()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var repo = new UserRepository(db);
        var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == "akram");
        user!.Password = "editPassword";
        var resultApdate = await repo.UpdateUserAsync(user);
        var result = await db.Users.FirstOrDefaultAsync(x => x.UserName == "akram");
        Assert.True(resultApdate);
        Assert.NotNull(result);
        Assert.Equal("editPassword",result.Password);
    }
    [Fact]
    public async Task UpdateUserAsync_WrongUser_ReturnFalse()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var repo = new UserRepository(db);
        var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == "akram");
        user!.UserName = "WrongUserName";
        var resultApdate = await repo.UpdateUserAsync(user);
        Assert.False(resultApdate);
    }
}
