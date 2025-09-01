using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.AdminRepository;
using System.Collections;

namespace BookingSystem.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminResponse>> GetAllAdmins();
        Task<AdminResponse?> GetAdminById(int id);
        Task<AdminResponse?> CreateAdmin(RegisterRequest request);
        Task<bool> DeleteAdmin(int id);
    }

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IPasswordService _passwordService;
        public AdminService(IAdminRepository adminRepository, IPasswordService passwordService )
        {
            _adminRepository = adminRepository;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<AdminResponse>> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAll();
            return admins.Select(MapToResponse);
        }
        public async Task<AdminResponse?> GetAdminById(int id)
        {
            var admin = await _adminRepository.GetById(id);
            return admin != null ? MapToResponse(admin) : null;
        }
        public async Task<AdminResponse?> CreateAdmin(RegisterRequest register)
        {
            if (await _adminRepository.UsernameExist(register.Username))
                return null;
            var password = _passwordService.HashPassword(register.Password);

            var admin = new Administrator
            {
                Username = register.Username,
                Password = password,

            };

            var createdAdmin = await _adminRepository.Create(admin);
            return MapToResponse(createdAdmin);
        }
        public async Task<bool> DeleteAdmin(int id)
        {
            return await _adminRepository.Delete(id);
        }

        private static AdminResponse MapToResponse(Administrator admin)
        {
            return new AdminResponse
            {
                AdminId = admin.AdminId,
                Username = admin.Username
            };
        }
    }
}
