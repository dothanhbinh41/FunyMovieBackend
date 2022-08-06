using FunyMovieBackend.DbContexts.Entities; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 
using System.Data.Common; 
namespace FunyMovieBackend.DbContexts
{ 
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    { 
        public virtual DbSet<Movie> Movies { get; set; }  
        public virtual DbSet<ReactionMovie> ReactionMovies { get; set; }  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 
        }
    }
}
