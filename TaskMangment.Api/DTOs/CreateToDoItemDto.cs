namespace TaskMangment.Api.DTOs;

public record class CreateToDoItemDto
(
    string Title ,
    string? Description,
    DateTime DueDate
);
