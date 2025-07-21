using MyApiApp.Models;

namespace MyApiApp.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAllAsync();
        Task<Comments?> GetByIdAsync(int id);
        Task<Comments> CreateAsync(Comments commentModel);
        Task<Comments?> UpdateAsync(int id, Comments commentModel);
        Task<Comments?> DeleteAsync(int id);
    }
}