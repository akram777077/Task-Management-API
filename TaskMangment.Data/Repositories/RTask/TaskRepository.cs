using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        var actUser = await _context.Users.AsNoTracking()
        .Include(x => x.Tasks)
        .FirstOrDefaultAsync(x => x.UserName == username);
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

    public async Task<bool> UpdateTaskFromUserAsync(TaskEntity newTask)
    {
        var result = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == newTask.Id);
        if (result is null)
            return false;
        _context.Entry(result)
        .CurrentValues
        .SetValues(newTask);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveTaskFromUserAsync(int taskId, string username)
    {
        var result = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result is null)
            throw new Exception("The task is not on the system");
        if (!result.Username.Equals(username))
            return false;
        _context.ToDoItems.Remove(result);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CompleteTaskOfUserAsync(int taskId, string username)
    {
        var result = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result is null)
            throw new Exception("The task is not on the system");
        if (!result.Username.Equals(username))
            return false;
        result.IsCompleted = true;
        _context.ToDoItems.Update(result);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReopenTaskOfUserAsync(int taskId, string username)
    {
        var result = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == taskId);
        if (result is null)
            throw new Exception("The task is not on the system");
        if (!result.Username.Equals(username))
            return false;
        result.IsCompleted = false;
        _context.ToDoItems.Update(result);
        await _context.SaveChangesAsync();
        return true;
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

    public async Task<TaskEntity?> GetTaskByUserAsync(int taskId, string username)
    {
        var result = await _context.ToDoItems
        .FirstOrDefaultAsync(x => x.Id == taskId);
        if (result is null || !result.Username.Equals(username))
            return null;
        return result;
    }
}