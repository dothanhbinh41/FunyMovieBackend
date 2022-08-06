using AutoMapper;
using FunyMovieBackend.Controllers.Dtos;
using FunyMovieBackend.DbContexts;
using FunyMovieBackend.DbContexts.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunyMovieBackend.Controllers
{
    public class IndexController : AppControllerBase
    {
        public IndexController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        [HttpGet("/movies")]
        public async Task<IActionResult> GetMoviesAsync()
        {
            var movies = DbContext.Movies.Include(d => d.User).ToList();
            return Ok(movies.Select(d =>
            new IndexMovieDto
            {
                Title = d.Title,
                Description = d.Description,
                ShareBy = d.User.UserName,
                Url = d.Link,
                YoutubeId = d.LinkId
            }));
        }
    }
}
