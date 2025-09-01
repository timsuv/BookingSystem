using BookingSystem.Models;

namespace BookingSystem.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        Task<Administrator?> GetById(int id);
        Task<Administrator?> GetByUsername(string username);
        Task<Administrator> Create(Administrator administrator);
        Task<Administrator> Update(Administrator administrator);
        Task<bool> Delete(int id);
        Task<IEnumerable<Administrator>> GetAll();
        Task<bool> UsernameExist(string username);
    }
}
