using BookingSystem.Data;
using BookingSystem.DTOs;
using BookingSystem.Models;
using BookingSystem.Repositories.TableRepository;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableResponse>> GetAllTables();
        Task<TableResponse?> GetTableById(int id);
        Task<TableResponse?> CreateTable(CreateTableRequest request);
        Task<TableResponse?> UpdateTable(int id, UpdateTableRequest request);
        Task<bool> DeleteTable(int id);
    }
    public class TableService : ITableService
    {
        private readonly ITableRepositiory _tableRepositiory;
        public TableService(ITableRepositiory tableRepositiory)
        {
            _tableRepositiory = tableRepositiory;
        }

        public async Task<IEnumerable<TableResponse>> GetAllTables()
        {
            var tables = await _tableRepositiory.GetAll();
            return tables.Select(MapToResponse);
        }
        public async Task<TableResponse?> GetTableById(int id)
        {
            var table = await _tableRepositiory.GetById(id);
            return table != null ? MapToResponse(table) : null;
        }
        public async Task<TableResponse?> CreateTable(CreateTableRequest request)
        {
            if (await _tableRepositiory.TableNumberExists(request.TableNumber))
                return null;
            var table = new Table
            {
                TableNumber = request.TableNumber,
                Capacity = request.Capacity
            };
            var createdTable = await _tableRepositiory.Create(table);
            return MapToResponse(createdTable);
        }
        public async Task<TableResponse?> UpdateTable(int id, UpdateTableRequest request)
        {
            var table = await _tableRepositiory.GetById(id);
            if (table == null)
                return null;

            table.TableNumber = request.TableNumber;
            table.Capacity = request.Capacity;

            var updatedTable = await _tableRepositiory.Update(table);
            return MapToResponse(updatedTable);
        }
        public async Task<bool> DeleteTable(int id)
        {
            if (await _tableRepositiory.HasActiveBookings(id))
                return false;

            return await _tableRepositiory.Delete(id);
        }

        private static TableResponse MapToResponse(Table table)
        {
            return new TableResponse
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity
            };
        }
    }
}
