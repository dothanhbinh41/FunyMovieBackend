using AutoMapper;
using FunyMovieBackend.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunyMovieBackend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AppControllerBase : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ApplicationDbContext DbContext => dbContext;

        public AppControllerBase(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string? UserId
        {
            get
            {
                {
                    if (User == null)
                    {
                        return null;
                    }
                    return User.FindFirstValue(ClaimTypes.NameIdentifier); 
                }
            }
        }
    }
}
