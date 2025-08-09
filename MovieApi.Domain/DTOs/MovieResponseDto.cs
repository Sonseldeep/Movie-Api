namespace MovieApi.Domain.DTOs;

public record MovieResponseDto(
    Guid Id,
    string Title,
    string? Genre,
    DateOnly ReleaseDate
    );