namespace TaskMangment.Api.DTOs;

public record  UpdateTaskWithUser(
    string Username,
    string Title,
    string? Description,
    DateTime DueDate,
    bool IsCompleted
);