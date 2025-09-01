using BookingSystem.Models;

namespace BookingSystem.Repositories.TableRepository
{
    public interface ITableRepositiory
    {
        Task<IEnumerable<Table>> GetAll();
        Task<Table?> GetById(int id);
        Task<Table> Create(Table table);
        Task<Table> Update(Table table);
        Task<bool> Delete(int id);
        Task<bool> TableNumberExists(int tableNumber, int? excludeTableId = null);
        Task<IEnumerable<Table>> GetTablesByCapacity(int minCapacity);
        Task<bool> HasActiveBookings(int tableId);
    }
}
