using FunyMovieBackend.DbContexts.Entities;

namespace FunyMovieBackend.Controllers.Dtos
{
    public class ReactionMovieRequestDto
    {
        public string MovieId { get; set; }
        public Reaction Reaction { get; set; }
    }
}