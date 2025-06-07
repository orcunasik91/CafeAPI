using CafeAPI.Application.Interfaces;
using CafeAPI.Domain.Entities;
using CafeAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Persistence.Repositories;
public class TableRepository(AppDbContext _context) : ITableRepository
{
    public async Task<List<Table>> GetActiveTablesAsync()
    {
        var result = await _context.Tables.Where(t => t.IsActive == true).ToListAsync();
        return result;
    }

    public async Task<Table> GetByTableNumberAsync(int tableNumber)
    {
        var result = await _context.Tables.FirstOrDefaultAsync(t => t.TableNumber == tableNumber);
        return result;
    }
}