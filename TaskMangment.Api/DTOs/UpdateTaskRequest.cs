namespace TaskMangment.Api.DTOs;

public record class UpdateTaskRequest
(
    string Title,
    string? Description,
    DateTime DueDate,
    bool IsCompleted
);
