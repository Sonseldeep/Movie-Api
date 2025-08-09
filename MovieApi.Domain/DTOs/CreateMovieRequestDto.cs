namespace MovieApi.Domain.DTOs;

public record CreateMovieRequestDto
(
    string Title,
    string Genre,
    DateOnly ReleaseDate
);
    
