using System;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Entities;

namespace TaskMangment.Api.Services.STask;

public interface ITaskService
{
    Task<List<ToDoItem>> GetAllTasksAsync();
    Task<ToDoItem?> GetTaskByIdAsync(int id);
    Task<ToDoItem> CreateTaskAsync(CreateToDoItemDto newTask);
    Task<bool> UpdateTaskAsync(UpdateToDoItemDto updatedTask);
    Task DeleteTaskAsync(int id);
    Task<bool> CompleteTaskAsync(int id);
    Task<bool> ReopenTaskAsync(int id);
}

