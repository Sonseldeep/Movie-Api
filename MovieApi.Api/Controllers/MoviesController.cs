

using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Api.Routes;
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
    
    [HttpGet(MovieEndpoints.Movies.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMovies([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
    {
        // filter garda 1st step is AsQueryable garaune
        var moviesQuery = _dbContext.Movies.AsNoTracking().AsQueryable();
        // 2nd sept is apply filter if provided

        if (!string.IsNullOrEmpty(filterQuery) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(filterQuery));
            }
        }
        // 3rd step is to execute the query
        var movies = await moviesQuery.ToListAsync();
        return Ok(movies.Select(m => m.ToResponseDto()));
    }

    [HttpGet(MovieEndpoints.Movies.Get)]
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

    [HttpPost(MovieEndpoints.Movies.Create)]
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


    [HttpPut(MovieEndpoints.Movies.Update)]
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

    [HttpDelete(MovieEndpoints.Movies.Delete)]
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