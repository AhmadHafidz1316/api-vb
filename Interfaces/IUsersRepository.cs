using MyApiApp.Models;

namespace MyApiApp.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllAsync();
    }
}