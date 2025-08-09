using System.ComponentModel.DataAnnotations;

namespace MovieApi.Domain.DTOs;

public record UpdateMovieRequestDto(
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    string Title,

    [Required(ErrorMessage = "Genre is required")]
    [MinLength(1, ErrorMessage = "At least one genre is required")]
    List<string> Genre,

    [Required(ErrorMessage = "ReleaseDate is required")]
    DateOnly ReleaseDate
);