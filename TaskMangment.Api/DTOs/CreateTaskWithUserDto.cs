namespace TaskMangment.Api.DTOs;

public record  CreateTaskWithUserDto(
    string Username,
    string Title,
    string? Description,
    DateTime DueDate
);
