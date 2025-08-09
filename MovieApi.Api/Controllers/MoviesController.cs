

using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieApi.Application.Mappers;

namespace MovieApi.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieDbContext _dbContext;

    public MoviesController(MovieDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet("/api/movies")]
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _dbContext.Movies.ToListAsync();
        return Ok(movies.Select(m => m.ToResponseDto()));
    }

    [HttpGet("/api/movies/{id:guid}")]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id)
    {
        var movie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie.ToResponseDto());
    }
}