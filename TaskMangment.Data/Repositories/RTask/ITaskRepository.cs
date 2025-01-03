using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RTask;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity?> GetTaskByIdAsync(int id);
    Task<TaskEntity> CreateTaskAsync(TaskEntity newTask);
    Task<bool> UpdateTaskAsync(TaskEntity updatedTask);
    Task<bool> DeleteTaskAsync(int id);
    Task<bool> CompleteTaskAsync(int id);
    Task<bool> ReopenTaskAsync(int id);
    Task<List<TaskEntity>> GetTasksByUserAsync(string username);
    Task<TaskEntity> AssignTaskToUserAsync(TaskEntity task);
    Task<bool> UpdateTaskFromUserAsync(TaskEntity newTask);
    Task<bool> RemoveTaskFromUserAsync(int taskId,string username);
    Task<bool> CompleteTaskOfUserAsync(int taskId,string username);
    Task<bool> ReopenTaskOfUserAsync(int taskId,string username);
}

