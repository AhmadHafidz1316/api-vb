using MyApiApp.Dtos.User;
using MyApiApp.Models;

namespace MyApiApp.Mappers
{
    public static class UsersMappers
    {
        public static UserDto  ToUsersDto(this Users users)
        {
            return new UserDto
            {
                Id = users.Id,
                Email = users.Email,
                UserName = users.UserName
            };
        }
    }
}