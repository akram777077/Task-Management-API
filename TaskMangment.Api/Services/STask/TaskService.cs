using System;
using Microsoft.EntityFrameworkCore;
using TaskMangment.Api.Data;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Entities;
using TaskMangment.Api.Mapping;

namespace TaskMangment.Api.Services.STask;

public class TaskService : ITaskService
{
    private readonly ToDoListDbContext _context;

    public TaskService(ToDoListDbContext context)
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

    public async Task<ToDoItem> CreateTaskAsync(CreateToDoItemDto newTask)
    {
        var task = newTask.ToEntity();
        _context.ToDoItems.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _context.ToDoItems
        .Where(x => x.Id == id)
        .ExecuteDeleteAsync();
    }

    public async Task<List<ToDoItem>> GetAllTasksAsync()
    {
        return await _context.ToDoItems
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<ToDoItem?> GetTaskByIdAsync(int id)
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

    public async Task<bool> UpdateTaskAsync(UpdateToDoItemDto updatedTask)
    {
        var result = await _context.ToDoItems.SingleOrDefaultAsync(x => x.Id == updatedTask.Id);
        if(result is null)
            return false;
        _context.Entry(result)
        .CurrentValues
        .SetValues(updatedTask.ToEntity());
        await _context.SaveChangesAsync();
        return true;
    }
}
