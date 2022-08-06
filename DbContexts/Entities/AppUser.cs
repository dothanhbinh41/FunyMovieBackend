using Microsoft.AspNetCore.Identity;

namespace FunyMovieBackend.DbContexts.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
