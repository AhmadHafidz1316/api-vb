using MyApiApp.Dtos.Stock;
using MyApiApp.Helpers;
using MyApiApp.Models;

namespace MyApiApp.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}