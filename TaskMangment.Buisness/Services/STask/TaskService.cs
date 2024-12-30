using System;
using Microsoft.EntityFrameworkCore.Query;
using TaskMangment.Buisness.Mapping;
using TaskMangment.Buisness.Models;
using TaskMangment.Data.Repositories.RTask;

namespace TaskMangment.Buisness.Services.STask;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        this._repository = repository;
    }
    public async Task<bool> CompleteAsync(int id)
    {
        if(id <= 0)
            throw new ArgumentException("Id must be greater than 0");
        return await _repository.CompleteTaskAsync(id);
    }

    public async Task<TaskModel> CreateAsync(TaskModel newTask)
    {
        if(newTask is {Id : null or not 0})
            throw new ArgumentException("Id must be 0");
        var entity = newTask.ToEntity();
        var result = await _repository.CreateTaskAsync(entity);
        return result.ToModel();
    }

    public async Task DeleteAsync(int id)
    {
        if(id <= 0 )
            throw new ArgumentException("Id must be greater than 0");
        await _repository.DeleteTaskAsync(id);
    }

    public async Task<List<TaskModel>> GetAllAsync()
    {
        var tasksEntites = await _repository.GetAllTasksAsync();
        var tasksModels = tasksEntites.Select(x => x.ToModel()).ToList();
        return tasksModels;
    }

    public async Task<TaskModel?> GetByIdAsync(int id)
    {
        if(id <= 0)
            throw new ArgumentException("Id must be greater than 0");
        var taskEntity = await _repository.GetTaskByIdAsync(id);
        return taskEntity?.ToModel();
    }

    public async Task<bool> ReopenAsync(int id)
    {
        if(id <= 0)
            throw new ArgumentException("Id must be greater than 0");
        return await _repository.ReopenTaskAsync(id);
    }

    public async Task<bool> UpdateAsync(TaskModel updatedTask)
    {
        if(updatedTask is {Id : null or  <= 0})
            throw new ArgumentException("Id must be greater than 0");
        var entity = updatedTask.ToEntity();
        var result =  await _repository.UpdateTaskAsync(entity);
        return result;
    }
}
