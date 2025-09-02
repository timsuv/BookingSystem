using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.MenuRepository;

namespace BookingSystem.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemResponse>> GetAllMenuItems();
        Task<MenuItemResponse?> GetMenuById(int id);
        Task<MenuItemResponse?> CreateMenuItem(CreateMenuItemRequest request);
        Task<MenuItemResponse?> UpdateMenuItem(int id, UpdateMenuItemRequest request);
        Task<IEnumerable<MenuItemResponse>> GetPopularMenuItem();
        Task<bool> DeleteMenuItem(int id);
    }


    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IEnumerable<MenuItemResponse>> GetAllMenuItems()
        {
            var menuItems = await _menuRepository.GetAll();
            return menuItems.Select(MapToResponse);
        }
        public async Task<MenuItemResponse?> GetMenuById(int id)
        {
            var menuItem = await _menuRepository.GetById(id);
            return menuItem != null ? MapToResponse(menuItem) : null;
        }
        public async Task<MenuItemResponse?> CreateMenuItem(CreateMenuItemRequest request)
        {
            var menuItem = new MenuItem
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                IsPopular = request.IsPopular,
                BildUrl = request.BildUrl,
            };
            var createdMenuItem = await _menuRepository.Create(menuItem);
            return MapToResponse(createdMenuItem);
        }

        public async Task<MenuItemResponse?> UpdateMenuItem(int id, UpdateMenuItemRequest request)
        {
            var menuItem = await _menuRepository.GetById(id);
            if (menuItem == null)
                return null;

            menuItem.Name = request.Name;
            menuItem.Price = request.Price;
            menuItem.Description = request.Description;
            menuItem.IsPopular = request.IsPopular;
            menuItem.BildUrl = request.BildUrl;

            var updatedeMenuItem = await _menuRepository.Update(menuItem);
            return MapToResponse(updatedeMenuItem);

        }
        public async Task<IEnumerable<MenuItemResponse>> GetPopularMenuItem()
        {
            var popularItems = await _menuRepository.GetPopularItems();
            return popularItems.Select(MapToResponse);

        }
        public async Task<bool> DeleteMenuItem(int id)
        {
            return await _menuRepository.Delete(id);
        }
        private static MenuItemResponse MapToResponse(MenuItem menuItem)
        {
            return new MenuItemResponse
            {
                MenuItemId = menuItem.MenuItemId,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Description = menuItem.Description,
                IsPopular = menuItem.IsPopular,
                BildUrl = menuItem.BildUrl,

            };
        }

    }
}
