using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.Login(request);
            if (result == null)
                return Unauthorized("Invalid username or password");
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _authService.UserExists(request.Username))
                return Conflict("Username already exists");
            var success = await _authService.Register(request);
            if (!success)
                return BadRequest("Failed to create admin");
            return Ok(new { message = "Admin created successfully" });

        }
        //endpoint to check if the admin is auth
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var adminId = User.FindFirst("AdminId")?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (adminId == null || username == null)
                return Unauthorized();
            return Ok(new { username = username });
        }


    }
}
