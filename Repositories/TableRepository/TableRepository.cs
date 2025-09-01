using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Repositories.TableRepository
{
    public class TableRepository : ITableRepositiory
    {
        private readonly RestaurantDbContext _context;
        public TableRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Table>> GetAll()
        {
            return await _context.Tables
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<Table?> GetById(int id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<Table> Create(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<Table> Update(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task<bool> Delete(int id)
        {
            var table = await GetById(id);
            if (table == null) return false;

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TableNumberExists(int tableNumber, int? excludeTableId = null)
        {
            return await _context.Tables
                .AnyAsync(t => t.TableNumber == tableNumber &&
                              (excludeTableId == null || t.TableId != excludeTableId));
        }

        public async Task<IEnumerable<Table>> GetTablesByCapacity(int minCapacity)
        {
            return await _context.Tables
                .Where(t => t.Capacity >= minCapacity)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();
        }

        public async Task<bool> HasActiveBookings(int tableId)
        {
            return await _context.Bookings
                .AnyAsync(b => b.TableId == tableId && b.BookingDate >= DateTime.Today);
        }
    }
}
