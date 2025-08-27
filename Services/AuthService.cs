using BookingSystem.Data;
using BookingSystem.DTOs;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingSystem.Services
{

    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        string GenerateJwtToken(Administrator admin);

    }
    public class AuthService : IAuthService
    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly IConfiguration _configuration;
        public AuthService(RestaurantDbContext context, IPasswordService passwordService, IConfiguration configuration)
        {
            _context = context;
            _passwordService = passwordService;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var admin = await _context.Administrators.FirstOrDefaultAsync(a => a.Username
             == request.Username);
            if (admin == null)
            {
                return null;
            }
            if (!_passwordService.VerifyPassword(request.Password, admin.Password))
                return null;

            var token = GenerateJwtToken(admin);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            return new LoginResponse
            {
                Token = token,
                Username = admin.Username,
                ExpiresAt = expiresAt
            };

        }

        public async Task<bool> Register(RegisterRequest request)
        {
            if (await _context.Administrators.AnyAsync(a => a.Username == request.Username))
            {
                return false;
            }

            var hashedPassword = _passwordService.HashPassword(request.Password);
            var newAdmin = new Administrator
            {
                Username = request.Username,
                Password = hashedPassword,

            };

            _context.Administrators.Add(newAdmin);
            await _context.SaveChangesAsync();

            return true;
        }

        private string GenerateJwtToken(Administrator admin)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("JWT Service not configured"));
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim("AdminId", admin.AdminId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        string IAuthService.GenerateJwtToken(Administrator admin)
        {
            return GenerateJwtToken(admin);
        }
    }
}
