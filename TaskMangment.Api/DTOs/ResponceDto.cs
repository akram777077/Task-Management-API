namespace TaskMangment.Api.DTOs;

public record class ResponseDto
(
    int Id,
    string Title,
    string? Description,
    DateTime DueDate,
    bool IsCompleted
);