using MovieApi.Domain.DTOs;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Mappers;

public static class MovieMapper
{
    public static MovieResponseDto ToResponseDto(this Movie movie)
    {
        return new MovieResponseDto(movie.Id, movie.Title, movie.Genre, movie.ReleaseDate);
    }

    public static Movie ToEntity(this CreateMovieRequestDto dto)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Genre = dto.Genre,
            ReleaseDate = dto.ReleaseDate
        };

    }

}