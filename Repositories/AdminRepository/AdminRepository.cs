using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly RestaurantDbContext _context;
        public AdminRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Administrator?> GetById(int id)
        {
            return await _context.Administrators.FindAsync(id);
        }
        public async Task<Administrator?> GetByUsername(string username)
        {
            return await _context.Administrators
                .FirstOrDefaultAsync(a=> a.Username == username);
        }
        public async Task<Administrator?> Create(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            await _context.SaveChangesAsync();
            return administrator;

        }
        public async Task<Administrator> Update(Administrator administrator)
        {
            _context.Administrators.Update(administrator);
            await _context.SaveChangesAsync();
            return administrator;
        }
        public async Task<bool> Delete(int id)
        {
            var admin = await GetById(id);
            if (admin == null) return false;

            _context.Administrators.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UsernameExist(string username)
        {
            return await _context.Administrators.AnyAsync(a => a.Username == username);

        }
        public async Task<IEnumerable<Administrator>> GetAll()
        {
            return await _context.Administrators.OrderBy(a  => a.Username)
                .ToListAsync();
        }
    }
}
