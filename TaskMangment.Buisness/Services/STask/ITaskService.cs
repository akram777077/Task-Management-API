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
}

