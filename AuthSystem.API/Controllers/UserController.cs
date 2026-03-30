using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
            {
                UserId = userId,
                Username = username,
                Email = email
            });

        }

        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult GetPublicInfo()
        {
            return Ok(new
            {
                Message = "This is a public endpoint accessible without authentication."
            });



        }
    }
}
