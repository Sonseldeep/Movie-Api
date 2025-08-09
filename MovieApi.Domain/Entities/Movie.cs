using System.Text.RegularExpressions;

namespace MovieApi.Domain.Entities;

public partial class Movie
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public required List<string> Genre { get; set; } = [];
    public DateOnly ReleaseDate { get; set; }
    
    
  
}