using System.ComponentModel.DataAnnotations;

namespace FunyMovieBackend.Controllers.Dtos
{
    public class ShareMovieRequestDto
    {
        [Required]
        public string Url { get; set; }
    }
}
