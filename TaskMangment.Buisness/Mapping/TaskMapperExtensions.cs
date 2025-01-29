using System;
using AutoMapper;
using TaskMangment.Buisness.Models;
using TaskMangment.Data.Entities;
namespace TaskMangment.Buisness.Mapping;

public static class TaskMapperExtensions
{
    public static TaskModel ToModel(this TaskEntity entity)
    {
        return new TaskModel(
            entity.Username,
            entity.Id,
            entity.Title,
            entity.Description,
            entity.DueDate,
            entity.IsCompleted
        );
    }

    public static TaskEntity ToEntity(this TaskModel model)
    {

        return new TaskEntity
        {
            Username = model.Username,
            Id = model.Id is null ? 0 : (int)model.Id,
            Title = model.Title,
            Description = model.Description,
            DueDate = model.DueDate,
            IsCompleted = model.IsCompleted
        };
    }
}
