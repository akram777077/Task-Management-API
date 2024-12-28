namespace TaskMangment.Api.DTOs;

public record class UpdateToDoItemDto
(
    int Id,
    string Title ,
    string? Description,
    DateTime DueDate
);
