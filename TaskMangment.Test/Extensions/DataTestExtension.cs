using System;
using TaskMangment.Data.Entities;
using TaskMangment.Test.Data;

namespace TaskMangment.Test.Extensions;

public static class DataTestExtension
{
    public static Task<List<TaskEntity>> getListAsync()
    {
        var result =  new List<TaskEntity> ()
        {
            new TaskEntity()
            {
                Id = 1,
                Title = "Title1",
                Description = "Description1",
                DueDate = DateTime.Now,
                IsCompleted = false
            },
            new TaskEntity()
            {
                Id = 2,
                Title = "Title2",
                Description = "Description2",
                DueDate = DateTime.Now,
                IsCompleted = true
            },
            new TaskEntity()
            {
                Id = 3,
                Title = "Title3",
                Description = "Description3",
                DueDate = DateTime.Now,
                IsCompleted = false
            },
            new TaskEntity()
            {
                Id = 4,
                Title = "Title4",
                Description = "Description4",
                DueDate = DateTime.Now,
                IsCompleted = false
            }
        };
        return Task.FromResult(result);
    }

    public static async Task AddTestDataAsync(this InMemoryDbContext context)
    {
        var list = await getListAsync();
        context.ToDoItems.AddRange(list);

        await context.SaveChangesAsync();
    }
}
