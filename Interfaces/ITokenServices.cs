using MyApiApp.Models;

namespace MyApiApp.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(Users user);
    }
}