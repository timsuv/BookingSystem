using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var items = await _menuService.GetAllMenuItems();
            if (items == null)
                return NotFound("No items on the menu found");

            return Ok(items);

        }
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularItems()
        {
            var popularItems = await _menuService.GetPopularMenuItem();
            return Ok(popularItems);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var menuItem = await _menuService.GetMenuById(id);
            if (menuItem == null) return NotFound();
            return Ok(menuItem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItem = await _menuService.CreateMenuItem(request);

            if (menuItem == null) return BadRequest("Could not create menu item");

            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuItemId });

        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateMenuItems (int id, UpdateMenuItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menuItem = await _menuService.UpdateMenuItem(id, request);

            if (menuItem == null)
                return NotFound("Menu item not found");
            return Ok(menuItem);
        }

        [HttpDelete ("{id:int}")]
        [Authorize]
        public  async Task<IActionResult> DeleteMenuItem(int id)
        {
            var success = await _menuService.DeleteMenuItem(id);
            if(!success)
                return NotFound();

            return NoContent();
        }
    }
}
