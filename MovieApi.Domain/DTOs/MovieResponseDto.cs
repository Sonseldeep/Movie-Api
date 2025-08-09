namespace MovieApi.Domain.DTOs;

public record MovieResponseDto(
    Guid Id,
    string Title,
    List<string> Genre,
    DateOnly ReleaseDate
    );