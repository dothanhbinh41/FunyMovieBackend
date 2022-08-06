using AutoMapper;
using FunyMovieBackend.Controllers.Dtos;
using FunyMovieBackend.DbContexts;
using FunyMovieBackend.DbContexts.Entities;
using FunyMovieBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FunyMovieBackend.Controllers
{
    [Authorize]
    public class UserController : AppControllerBase
    {
        private readonly IMovieService movieService;

        public UserController(ApplicationDbContext dbContext, IMovieService movieService) : base(dbContext)
        {
            this.movieService = movieService;
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareMovieAsync(ShareMovieRequestDto request)
        {
            var movie = await movieService.GetMovieDetailAsync(request.Url);
            if (movie == null)
            {
                return BadRequest();
            }
            movie.UserId = UserId!;
            var result = DbContext.Movies.Add(movie);
            await DbContext.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpPost("reaction")]
        public async Task<IActionResult> ReactionMovieAsync(ReactionMovieRequestDto request)
        {
            var movie = DbContext.Movies.FirstOrDefault(d => d.Id == request.MovieId);
            if (movie == null)
            {
                return BadRequest();
            }

            var reactionExisted = DbContext.ReactionMovies.FirstOrDefault(d => d.MovieId == request.MovieId && d.UserId == UserId);
            if (reactionExisted == null)
            {
                await ProcessNormalReactionAsync(movie, request.Reaction);
                return Ok(true);
            }

            await ProcessExistedReactionAsync(reactionExisted, movie, request.Reaction);
            return Ok(true);
        }

        async Task ProcessNormalReactionAsync(Movie mv, Reaction reaction)
        {
            DbContext.ReactionMovies.Add(new ReactionMovie { UserId = UserId!, MovieId = mv.Id, Reaction = reaction });
            if (reaction == Reaction.Like)
            {
                mv.Like++;
            }
            else
            {
                mv.Unlike++;
            }
            DbContext.Movies.Update(mv);
            var result = await DbContext.SaveChangesAsync();
        }

        async Task ProcessExistedReactionAsync(ReactionMovie ractionMovie, Movie movie, Reaction reaction)
        {
            if (ractionMovie.Reaction == reaction)
            {
                DbContext.ReactionMovies.Remove(ractionMovie);
                if (reaction == Reaction.Like)
                {
                    movie.Like--;
                }
                else
                {
                    movie.Unlike--;
                }
            }
            else
            {
                ractionMovie.Reaction = reaction;
                DbContext.ReactionMovies.Update(ractionMovie);
                if (reaction == Reaction.Like)
                {
                    movie.Like++;
                    movie.Unlike--;
                }
                else
                {
                    movie.Like--;
                    movie.Unlike++;
                }
            }

            DbContext.Movies.Update(movie);
            var result = await DbContext.SaveChangesAsync();
        }

    }
}
