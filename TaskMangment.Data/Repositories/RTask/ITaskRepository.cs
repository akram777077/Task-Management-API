using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RTask;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity?> GetTaskByIdAsync(int id);
    Task<TaskEntity> CreateTaskAsync(TaskEntity newTask);
    Task<bool> UpdateTaskAsync(TaskEntity updatedTask);
    Task DeleteTaskAsync(int id);
    Task<bool> CompleteTaskAsync(int id);
    Task<bool> ReopenTaskAsync(int id);
}

