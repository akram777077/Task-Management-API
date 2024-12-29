using System;

namespace TaskMangment.Buisness.Models;

public readonly struct TaskModel
{
    public TaskModel(int? id, string title, string? description, DateTime dueDate, bool isCompleted = false)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = isCompleted;
    }

    public readonly  int? Id ;
    public readonly string Title;
    public readonly string? Description;
    public readonly DateTime DueDate;
    public readonly bool IsCompleted;
}
