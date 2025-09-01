using BookingSystem.Data;
using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.AdminRepository;
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
        Task<bool> UserExists(string username);

    }
    public class AuthService : IAuthService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(IAdminRepository adminRepository, IPasswordService passwordService, IJwtTokenService jwtTokenService)
        {
            _adminRepository = adminRepository;
            _passwordService = passwordService;
            _jwtTokenService = jwtTokenService;

        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var admin = await _adminRepository.GetByUsername(request.Username);
            if (admin == null)
                return null;

            if (!_passwordService.VerifyPassword(request.Password, admin.Password))
                return null;

            var token = _jwtTokenService.GenerateToken(admin);
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
            if (await _adminRepository.UsernameExist(request.Username))
            {
                return false;
            }

            var hashedPassword = _passwordService.HashPassword(request.Password);
            var newAdmin = new Administrator
            {
                Username = request.Username,
                Password = hashedPassword,

            };

            await _adminRepository.Create(newAdmin);
            return true;
        }
        public async Task<bool> UserExists(string username)
        {
            return await _adminRepository.UsernameExist(username);
        }


    }
}
