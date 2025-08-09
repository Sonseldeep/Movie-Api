namespace MovieApi.Domain.Entities;

public class Movie
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
}