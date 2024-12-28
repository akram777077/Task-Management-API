using System;
using TaskMangment.Api.Entities;
using TaskMangment.Test.Data;

namespace TaskMangment.Test.Extension;

public static class ListToDoTest
{
    public static List<ToDoItem> getList()
    {
        return new List<ToDoItem>()
        {
            new ToDoItem()
            {
                Id = 1,
                Title = "Test1",
                Description = "Description1",
                DueDate = DateTime.Now,
                IsCompleted = false
            },
            new ToDoItem()
            {
                Id = 2,
                Title = "Test2",
                Description = "Description2",
                DueDate = new DateTime(2023, 12, 12),
                IsCompleted = true
            },
            new ToDoItem()
            {
                Id = 3,
                Title = "Test3",
                Description = null,
                DueDate = DateTime.Now,
                IsCompleted = false
            },
            new ToDoItem()
            {
                Id = 4,
                Title = "Test4",
                Description = "Description4",
                DueDate = new DateTime(2024, 12, 12),
                IsCompleted = true
            }
        };
    }
    public static async Task  AddTestDataAsync(this InMemoryDbContext db)
    {
        db.AddRange(getList());
        await db.SaveChangesAsync();
    }
}