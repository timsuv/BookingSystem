using BookingSystem.DTOs;
using BookingSystem.Repositories.AdminRepository;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]

    public class AdminsController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins() 
        {
            var admins = await _adminService.GetAllAdmins();
            if (admins == null) 
                return NotFound("No admins found, that is crucial because the program doesn't work without admins");
            return Ok(admins);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _adminService.GetAdminById(id);

            if (admin == null)
                return NotFound("There are no admin with such id");

            return Ok(admin);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAdmin(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = await _adminService.CreateAdmin(request);

            if (admin == null)
                return Conflict("Username exists already");

            return CreatedAtAction(nameof(GetAdminById), new { id = admin });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var currentAdmin = User.FindFirst("AdminId")?.Value;
            if (currentAdmin != null && int.Parse(currentAdmin) == id)
                return BadRequest("Cannot delete your own account");

            var success = await _adminService.DeleteAdmin(id);
            if (!success) return NotFound();

            return Ok("Admin deleted");
        }


    }
}
