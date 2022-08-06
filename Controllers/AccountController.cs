using AutoMapper;
using FunyMovieBackend.Controllers.Dtos;
using FunyMovieBackend.DbContexts;
using FunyMovieBackend.DbContexts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FunyMovieBackend.Controllers
{

    public class AccountController : AppControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> userManager, ApplicationDbContext applicationDb) : base(applicationDb)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var existedUser = await userManager.FindByNameAsync(request.UserName);
            if (existedUser != null)
            {
                return BadRequest("");
            }
            var result = await userManager.CreateAsync(new AppUser { UserName = request.UserName }, request.Password);
            if (result.Succeeded)
            {
                return Ok("");
            }
            return BadRequest(result.Errors);
        }
    }
}
