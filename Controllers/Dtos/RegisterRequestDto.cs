using System.ComponentModel.DataAnnotations;

namespace FunyMovieBackend.Controllers.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}