using System;
using System.Diagnostics.CodeAnalysis;
using TaskMangment.Data.Entities;
using TaskMangment.Test.Data;
using Xunit.Sdk;

namespace TaskMangment.Test.Extensions;
[ExcludeFromCodeCoverage]

public static class DataTestExtension
{
    public static Task<List<UserEntity>> getListUserAsync()
    {
        var result = new List<UserEntity>()
        {
            new UserEntity()
            {
                UserName = "akram",
                Password = "password1",
                Tasks = new List<TaskEntity>()
                {
                    new TaskEntity()
                    {
                        Id = 1,
                        Title = "Task 1",
                        Description = "Description of Task 1",
                        DueDate = new DateTime(2025, 12, 11, 0, 0, 1),
                        IsCompleted = false,
                        Username = "akram"
                    },
                    new TaskEntity()
                    {
                        Id = 2,
                        Title = "Task 2",
                        Description = "Description of Task 2",
                        DueDate = new DateTime(2025, 2, 1, 23, 12, 1),
                        IsCompleted = true,
                        Username = "akram"
                    }
                }
            },
            new UserEntity()
            {
                UserName = "john",
                Password = "password2",
                Tasks = new List<TaskEntity>()
                {
                    new TaskEntity()
                    {
                        Id = 3,
                        Title = "Task 1",
                        Description = "Description of Task 1",
                        DueDate = DateTime.Now.AddDays(3),
                        IsCompleted = false,
                        Username = "john"
                    },
                    new TaskEntity()
                    {
                        Id = 4,
                        Title = "Task 2",
                        Description = "Description of Task 2",
                        DueDate = DateTime.Now.AddDays(10),
                        IsCompleted = true,
                        Username = "john"
                    },
                    new TaskEntity()
                    {
                        Id = 5,
                        Title = "Task 3",
                        Description = "Description of Task 3",
                        DueDate = DateTime.Now.AddDays(14),
                        IsCompleted = false,
                        Username = "john"
                    }
                }
            },
            new UserEntity()
            {
                UserName = "jane",
                Password = "password3",
                Tasks = new List<TaskEntity>()
                {
                    new TaskEntity()
                    {
                        Id = 6,
                        Title = "Task 1",
                        Description = "Description of Task 1",
                        DueDate = DateTime.Now.AddDays(1),
                        IsCompleted = false,
                        Username = "jane"
                    },
                    new TaskEntity()
                    {
                        Id = 7,
                        Title = "Task 2",
                        Description = "Description of Task 2",
                        DueDate = DateTime.Now.AddDays(8),
                        IsCompleted = false,
                        Username = "jane",
                    },
                    new TaskEntity()
                    {
                        Id = 8,
                        Title = "Task 3",
                        Description = "Description of Task 3",
                        DueDate = DateTime.Now.AddDays(12),
                        IsCompleted = true,
                        Username = "jane"
                    }
                }
            }
        };
        foreach (var user in result)
        {
            foreach (var task in user.Tasks)
            {
                task.User = user;
            }
        }

        return Task.FromResult(result);
    }

    public static async Task AddTestDataAsync(this InMemoryDbContext context)
    {
        context.Users.AddRange(await getListUserAsync());
        await context.SaveChangesAsync();
    }

    public static bool Same(this TaskEntity taskEntity, TaskEntity taskEntity2)
    {
        if (taskEntity.IsCompleted != taskEntity2.IsCompleted)
            return false;
        if (taskEntity.Title != taskEntity2.Title)
            return false;
        if (taskEntity.Description != taskEntity2.Description)
            return false;
        if (taskEntity.DueDate != taskEntity2.DueDate)
            return false;
        if (taskEntity.Username != taskEntity2.Username)
            return false;
        if ((taskEntity.User?.Password ?? "") != (taskEntity2.User?.Password ?? ""))
            return false;
        return true;
    }
}
