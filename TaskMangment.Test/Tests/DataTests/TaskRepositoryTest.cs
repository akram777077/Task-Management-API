using Microsoft.EntityFrameworkCore;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Repositories.RTask;
using TaskMangment.Test.Data;
using TaskMangment.Test.Extensions;

namespace TaskMangment.Test.Tests.DataTests;


public class TaskRepositoryTest 
{
    //? GetAllTasksAsync Tests
    [Fact]
    public async Task GetAllTasksAsync_ReturnsEmpty()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var service = new TaskRepository(db);
        var result = await  service.GetAllTasksAsync();
        Assert.NotNull(result);
        Assert.Empty(result);
        db.Dispose();
    }
    [Fact]
    public async Task GetAllTasksAsync_ReturnsAllTasks()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await  service.GetAllTasksAsync();
        Assert.NotNull(result);
        Assert.Equal(8, result.Count);
        db.Dispose();
    }
    
    //? GetTaskByIdAsync Tests
    [Fact]
    public async Task GetTaskById_ReturnsCorrectTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await  service.GetTaskByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        db.Dispose();
    }
    [Fact]
    public async Task GetTaskById_ReturnsNull()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await  service.GetTaskByIdAsync(-1);
        Assert.Null(result);
        db.Dispose();
    }
    
    //? CreateTaskAsync Tests
    [Fact]
    public async Task CreateTaskAsync_NonNullDescription_ReturnsCorrectTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.CreateTaskAsync(
            new ()
            {
                Title = "akram",
                Description = "description",
                DueDate = DateTime.Now,
                Username = "akram"
            }
        );
        Assert.NotNull(result);
        Assert.Equal(9, result.Id);
        db.Dispose();
    }
    [Fact]
    public async Task CreateTaskAsync_NullDescription_ReturnsCorrectTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var listUsers = await DataTestExtension.getListUserAsync();
        var result = await service.CreateTaskAsync(
            new ()
            {
                Title = "akram",
                Description = null,
                DueDate = DateTime.Now,
                Username = "akram"
            }
        );
        Assert.NotNull(result);
        Assert.Equal(9, result.Id);
        db.Dispose();
    }

    //? UpdateTaskAsync Tests
    [Fact]
    public async Task UpdateTaskAsync_AllFiledWithNonNullDiscription_ReturnsTrue_CheckTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var dateTest = DateTime.Now;
        var listUsers = await DataTestExtension.getListUserAsync();
        var result = await service.UpdateTaskAsync(
            new ()
            {
                Id = 1,
                Title = "Update",
                Description = "UpdateDescription",
                DueDate = dateTest,
                User = listUsers[0],
                Username = "akram"
            }
        );
        Assert.True(result);
        var updatedTask = await service.GetTaskByIdAsync(1);
        Assert.NotNull(updatedTask);
        Assert.Equal("Update", updatedTask.Title);
        Assert.Equal("UpdateDescription", updatedTask.Description);
        Assert.Equal(dateTest, updatedTask.DueDate);
        db.Dispose();
    }

    [Fact]
    public async Task UpdateTaskAsync_AllFiledWithNullDiscription_ReturnsTrue_CheckTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var dateTest = DateTime.Now;
        var listUsers = await DataTestExtension.getListUserAsync();
        var result = await service.UpdateTaskAsync(
            new (){
                Id = 1,
                Title = "Update",
                Description = null,
                DueDate = dateTest,
                User = listUsers[0],
                Username = "akram"
            }
        );
        Assert.True(result);
        var updatedTask = await service.GetTaskByIdAsync(1);
        Assert.NotNull(updatedTask);
        Assert.Equal("Update", updatedTask.Title);
        Assert.Null(updatedTask.Description);
        Assert.Equal(dateTest, updatedTask.DueDate);
        db.Dispose();
    }

    [Fact]
    public async Task UpdateTaskAsync_WrongId_ReturnsFalse_KeepsOldTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var dateTest = DateTime.Now;
        var result = await service.UpdateTaskAsync(
            new ()
            {
                Id = -1,
                Title = "Update",
                Description = "UpdateDescription",
                DueDate = dateTest
            }
        );
        Assert.False(result);
        db.Dispose();
    }

    //? DeleteTaskAsync Tests
    [Fact]
    public async Task DeleteTaskAsync_CorrectId()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var resultDelete=await service.DeleteTaskAsync(1);
        Assert.True(resultDelete);
        var result = await service.GetTaskByIdAsync(1);
        Assert.Null(result);
        db.Dispose();
    }
    //? DeleteTaskAsync Tests
    [Fact]
    public async Task DeleteTaskAsync_WrongId()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var resultDelete=await service.DeleteTaskAsync(-1);
        Assert.False(resultDelete);
        db.Dispose();
    }
    
    //? CompleteTaskAsync Tests
    [Fact]
    public async Task CompleteTaskAsync_CorrectId_ReturnsTrue()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.CompleteTaskAsync(1);
        Assert.True(result);
        var task = await service.GetTaskByIdAsync(1);
        Assert.NotNull(task);
        Assert.True(task.IsCompleted);
        db.Dispose();
    }

    [Fact]
    public async Task CompleteTaskAsync_WrongId_ReturnsFalse()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.CompleteTaskAsync(-1);
        Assert.False(result);
        db.Dispose();
    }

    //? ReopenTaskAsync Tests
    [Fact]
    public async Task ReopenTaskAsync_CorrectId_ReturnsTrue()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.ReopenTaskAsync(2);
        Assert.True(result);
        var task = await service.GetTaskByIdAsync(2);
        Assert.NotNull(task);
        Assert.False(task.IsCompleted);
        db.Dispose();
    }

    [Fact]
    public async Task ReopenTaskAsync_WrongId_ReturnsFalse()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.ReopenTaskAsync(-1);
        Assert.False(result);
        db.Dispose();
    }

    [Fact]
    public async Task GetTasksByUserAsync_CorrectUsername_ReturnListTasks()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.GetTasksByUserAsync("akram");
        var listTest = await DataTestExtension.getListUserAsync();
        var listTestTasks = listTest[0].Tasks.ToList();
        Assert.Equal(listTestTasks.Count, result.Count);
        listTestTasks = listTestTasks.OrderBy(t => t.Id).ToList();
        result = result.OrderBy(t => t.Id).ToList();
        for (int i = 0; i < result.Count; i++)
        {
            var a = result[i];
            var b = listTestTasks[i];
            Assert.True(a.Same(b));
        }
        db.Dispose();
    }

    [Fact]
    public async Task GetTasksByUserAsync_WrongUsername_Exception()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var service = new TaskRepository(db);
        var exception = await Assert.ThrowsAsync<Exception>(() => service.GetTasksByUserAsync("akram"));
        Assert.Equal("the user is not on the system",exception.Message);
        db.Dispose();   
    }
    [Fact]
    public async Task AssignTaskToUserAsync_CorrectTaskAndUser_ReturnsValidTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var service = new TaskRepository(db);
        var task = new TaskEntity
        {
            Title = "akramTask",
            Description = "description",
            DueDate = DateTime.Now,
            Username = "akram"
        };
        var user = new UserEntity()
        {
            UserName = "akram",
            Password = "test"
        };
        db.Users.Add(user);
        var result = await service.AssignTaskToUserAsync(task);
        Assert.Equal(1, result.Id);
        db.Dispose();
    }

    [Fact]
    public async Task RemoveTaskFromUserAsync_CorrectTaskAndUser_ReturnsTrue()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.RemoveTaskFromUserAsync(1, "akram");
        Assert.Null(await db.ToDoItems.FirstOrDefaultAsync(t => t.Id == 1));
        Assert.True(result);
        db.Dispose();
    }
    [Fact]
    public async Task RemoveTaskFromUserAsync_WrongTaskAndCorrectUser_ThrowsException()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var exception = await Assert.ThrowsAsync<Exception>(() => service.RemoveTaskFromUserAsync(-1, "akram"));
        Assert.Equal("The task is not on the system",exception.Message);
        db.Dispose();
    }
    [Fact]
    public async Task RemoveTaskFromUserAsync_CorrectTaskAndWrongUser_ThrowsException()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        var result = await service.RemoveTaskFromUserAsync(1, "john");
        Assert.False(result);
        db.Dispose();
    }
}