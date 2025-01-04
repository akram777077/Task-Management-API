using Microsoft.EntityFrameworkCore;
using TaskMangment.Data.Entities;

namespace TaskMangment.Data.Repositories.RTask;

public class TaskRepository : ITaskRepository
{
    private readonly ToDoListDbContext _context;

    public TaskRepository (ToDoListDbContext context)
    {
        this._context = context;
    }

    public async Task<bool> CompleteTaskAsync(int id)
    {
        var target = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
        if(target is null)
            return false;
        target.IsCompleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TaskEntity> CreateTaskAsync(TaskEntity newTask)
    {
        _context.ToDoItems.Add(newTask);
        await _context.SaveChangesAsync();
        return newTask;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var target = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
        if (target is null)
            return false;
        _context.ToDoItems.Remove(target);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TaskEntity>> GetAllTasksAsync()
    {
        return await _context.ToDoItems
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<TaskEntity?> GetTaskByIdAsync(int id)
    {
        return await _context.ToDoItems
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ReopenTaskAsync(int id)
    {
        var target = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == id);
        if(target is null)
            return false;
        target.IsCompleted = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TaskEntity>> GetTasksByUserAsync(string username)
    {
        var actUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (actUser is null)
            throw new Exception("the user is not on the system");
        var list = actUser.Tasks;
        return list.ToList();
    }

    public async Task<TaskEntity> AssignTaskToUserAsync(TaskEntity task)
    {
        _context.ToDoItems.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public Task<bool> UpdateTaskFromUserAsync(TaskEntity newTask)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveTaskFromUserAsync(int taskId, string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CompleteTaskOfUserAsync(int taskId, string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ReopenTaskOfUserAsync(int taskId, string username)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateTaskAsync(TaskEntity updatedTask)
    {
        var result = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == updatedTask.Id);
        if(result is null)
            return false;
        _context.Entry(result)
        .CurrentValues
        .SetValues(updatedTask);
        await _context.SaveChangesAsync();
        return true;
    }
}