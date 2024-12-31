namespace TaskMangment.Api.DTOs;

public record ErrorResponseDto
(
    int Status,
    string Title,
    string Description,
    DateTime Time
);
