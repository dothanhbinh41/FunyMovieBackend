using System.ComponentModel.DataAnnotations.Schema;

namespace FunyMovieBackend.DbContexts.Entities
{
    public enum Reaction
    {
        Like, Unlike
    }
    public class ReactionMovie : EntityBase
    {
        public string MovieId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie? Movie { get; set; }
        public Reaction Reaction { get; set; }
        public bool IsActived { get; set; }
    }
}
