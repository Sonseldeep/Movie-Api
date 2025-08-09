using System.ComponentModel.DataAnnotations;

namespace MovieApi.Domain.DTOs;


public record UpdateMovieRequestDto(
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    string Title,
    [Required(ErrorMessage = "Genre is required")]
    [StringLength(20, ErrorMessage = "Genre cannot be longer than 20 characters")]
    string Genre,
    [Required(ErrorMessage = "ReleaseDate is required")]
    DateOnly ReleaseDate
);
