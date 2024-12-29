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

    public async Task DeleteTaskAsync(int id)
    {
        await _context.ToDoItems
        .Where(x => x.Id == id)
        .ExecuteDeleteAsync();
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