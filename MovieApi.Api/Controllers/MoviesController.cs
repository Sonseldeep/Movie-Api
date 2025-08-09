

using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MovieApi.Application.Mappers;
using MovieApi.Domain.DTOs;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
   
   
    public async Task<IActionResult> GetMovies()
    {
        var movies = await _dbContext.Movies.ToListAsync();
        return Ok(movies.Select(m => m.ToResponseDto()));
    }

    [HttpGet("/api/movies/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id)
    {
        var movie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie.ToResponseDto());
    }

    [HttpPost("/api/movies")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequestDto dto  )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // mapping DTO to entity
        var movie = dto.ToEntity();
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie.ToResponseDto());
    }


    [HttpPut("/api/movies/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> UpdateMovie([FromRoute] Guid id, [FromBody] UpdateMovieRequestDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var movie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return NotFound();
        }
        dto.UpdateEntity(movie);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("/api/movies/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteMovie([FromRoute] Guid id)
    {
        var movie = await _dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);
        if (movie is null)
        {
            return NotFound();
        }
        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}