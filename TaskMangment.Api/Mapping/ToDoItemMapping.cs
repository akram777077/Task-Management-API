using System;
using TaskMangment.Api.DTOs;
using TaskMangment.Api.Entities;

namespace TaskMangment.Api.Mapping;

public static class ToDoItemMapping
{
    public static ToDoItem ToEntity(this CreateToDoItemDto item)
    {
        return new ()
        {
            Id = 0,
            Title = item.Title,
            Description = item.Description,
            DueDate = item.DueDate,
            IsCompleted= false
        };
    }
    public static ToDoItem ToEntity(this UpdateToDoItemDto item)
    {
        return new ()
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            DueDate = item.DueDate,
            IsCompleted= false
        };
    }
}
