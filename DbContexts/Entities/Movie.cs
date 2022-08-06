using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunyMovieBackend.DbContexts.Entities
{
    public class Movie : EntityBase
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? LinkId { get; set; }
        public string? UserId { get; set; }
        public uint Like { get; set; }
        public uint Unlike { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser? User { get; set; }
    }
}
