using Microsoft.EntityFrameworkCore;
using MovieApi.Domain.Entities;

namespace Infrastructure.Data;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
        
    }

    public DbSet<Movie> Movies { get; set; }
    
}