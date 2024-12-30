namespace TaskMangment.Api.DTOs;

public record class CreateTaskRequest
(
    string Title,
    string? Description,
    DateTime DueDate
);