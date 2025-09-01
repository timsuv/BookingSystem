using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories.MenuRepository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantDbContext _context;

        public MenuRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAll()
        {
            return await _context.MenuItems
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<MenuItem?> GetById(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        public async Task<MenuItem> Create(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> Update(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<bool> Delete(int id)
        {
            var menuItem = await GetById(id);
            if (menuItem == null) return false;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MenuItem>> GetPopularItems()
        {
            return await _context.MenuItems
                .Where(m => m.IsPopular)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }
    }
}

