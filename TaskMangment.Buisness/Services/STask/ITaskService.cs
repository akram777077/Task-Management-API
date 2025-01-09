using System;
using TaskMangment.Buisness.Models;

namespace TaskMangment.Buisness.Services.STask;

public interface ITaskService
{
    Task<List<TaskModel>> GetAllAsync();
    Task<TaskModel?> GetByIdAsync(int id);
    Task<TaskModel> CreateAsync(TaskModel newTask);
    Task<bool> UpdateAsync(TaskModel updatedTask);
    Task<bool> DeleteAsync(int id);
    Task<bool> CompleteAsync(int id);
    Task<bool> ReopenAsync(int id);
    Task<List<TaskModel>> GetByUserAsync(string username);
    Task<TaskModel> AssignToUserAsync(TaskModel task);
    Task<bool> UpdateFromUserAsync(TaskModel newTask);
    Task<bool> RemoveFromUserAsync(int taskId,string username);
    Task<bool> CompleteOfUserAsync(int taskId,string username);
    Task<bool> ReopenOfUserAsync(int taskId,string username);
}

