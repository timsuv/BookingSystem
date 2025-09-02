using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var tables = await _tableService.GetAllTables();
            if (tables == null)
            {
                return NotFound(new { message = "No tables found" });
            }
            return Ok(tables);
        }
        [HttpGet("{int:id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableService.GetTableById(id);
            if (table == null)
                return NotFound();
            return Ok(table);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTable(CreateTableRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var table = await _tableService.CreateTable(request);
            if (table == null)
                return Conflict("Table number already exists");

            return CreatedAtAction(nameof(GetTableById), new { id = table.TableId }, table);
        }
        [HttpPut("{int:id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTable(int id, UpdateTableRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var table = await _tableService.UpdateTable(id, request);

            if (table == null)
                return NotFound("Table not found or table number already exists");
            return Ok(table);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteTbale(int id)
        {
            var success = await _tableService.DeleteTable(id);

            if(!success)
                return BadRequest("Cannot delete table with active bookings");

            return NoContent();
        }


    }
}
