using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFFAPI.Data;
using SFFAPI.Models;

namespace SFFAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        public MoviesController(DataContext context)
        {
            _context = context;
        }

        // Get: api/v1/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // Get: api/v1/Movies/{id:int}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            
            return movie;
        }

        // Get: api/v1/Label/{movieId}/{studioId}
        [HttpGet("Label/{movieId}/{studioId}")]
        [Produces("application/xml")]
        public async Task<Label> GetLabel(int movieId, int studioId)
        {
            var label = new Label();
            var labelData = await label.CreateLabel(_context, movieId, studioId);
            var xml = label.LabelData(labelData);

            return xml;
        }

        // Post: api/v1/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movie.Id}, movie);
        }

        // Post: api/v1/PostTrivia/{movieId}/{studioId}
        [HttpPost("PostTrivia/{movieId}/{studioId}")]
        public async Task<ActionResult<Trivia>> PostTriviaWithMovieIdAndStudioId(int movieId, int studioId, Trivia trivia)
        {
            var movie = await _context.Movies.Where(m => m.Id == movieId).FirstOrDefaultAsync();
            var studio = await _context.Studios.Where(m => m.Id == studioId).FirstOrDefaultAsync();

            if (movie != null && studio != null)
            {
                if (trivia.Rating > 5 || trivia.Rating < 0)
                {
                    return StatusCode(400);
                }
                movie.AddTrivia(trivia, studio);
                await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            return StatusCode(400);
        }

        // Put: api/v1/Movies/{id:int}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // Delete: api/v1/Movies/RemoveTrivia/{id}
        [HttpDelete("RemoveTrivia/{id}")]
        public async Task<ActionResult<Trivia>> DeleteTrivia(int id)
        {
            var trivia = await _context.Trivias.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (trivia == null)
            {
                return NotFound();
            }

            _context.Trivias.Remove(trivia);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }
    }
}