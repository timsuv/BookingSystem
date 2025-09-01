using BookingSystem.Models;

namespace BookingSystem.Repositories.MenuRepository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuItem>> GetAll();
        Task<MenuItem?> GetById(int id);
        Task<MenuItem> Create(MenuItem menuItem);
        Task<MenuItem> Update(MenuItem menuItem);
        Task<bool> Delete(int id);
        Task<IEnumerable<MenuItem>> GetPopularItems();
    }
}
