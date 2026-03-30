using AuthSystem.API.DTOs;
using AuthSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public AuthController(IAuthService authService, ITokenBlacklistService tokenBlacklistService)
        {
            _authService = authService;
            _tokenBlacklistService = tokenBlacklistService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Get the token from the Authorization header
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            _tokenBlacklistService.BlacklistToken(token);

            return Ok((new { message = "Logged out successfully" }));
        }
    }
}
