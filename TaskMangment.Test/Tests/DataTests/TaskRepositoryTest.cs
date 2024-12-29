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
        Assert.Equal(4, result.Count);
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
        var result = await  service.GetTaskByIdAsync(5);
        Assert.Null(result);
        db.Dispose();
    }
    
    //? CreateTaskAsync Tests
    [Fact]
    public async Task CreateTaskAsync_NonNullDescription_ReturnsCorrectTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var service = new TaskRepository(db);
        var result = await service.CreateTaskAsync(
            new ()
            {
                Title = "akram",
                Description = "description",
                DueDate = DateTime.Now
            }
        );
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        db.Dispose();
    }
    [Fact]
    public async Task CreateTaskAsync_NullDescription_ReturnsCorrectTask()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        var service = new TaskRepository(db);
        var result = await service.CreateTaskAsync(
            new ()
            {
                Title = "akram",
                Description = null,
                DueDate = DateTime.Now
            }
        );
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
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
        var result = await service.UpdateTaskAsync(
            new ()
            {
                Id = 1,
                Title = "Update",
                Description = "UpdateDescription",
                DueDate = dateTest
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
        var result = await service.UpdateTaskAsync(
            new (){
                Id = 1,
                Title = "Update",
                Description = null,
                DueDate = dateTest
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
        var list = await DataTestExtension.getListAsync();
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
        Assert.Equal(list.Count, db.ToDoItems.Count());
        db.Dispose();
    }

    //? DeleteTaskAsync Tests
    [Fact]
    public async Task DeleteTaskAsync_CorrectId()
    {
        var db = InMemoryDbContext.CreateInMemoryDbContext();
        await db.AddTestDataAsync();
        var service = new TaskRepository(db);
        await service.DeleteTaskAsync(1);
        var result = await service.GetTaskByIdAsync(1);
        Assert.Null(result);
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
}