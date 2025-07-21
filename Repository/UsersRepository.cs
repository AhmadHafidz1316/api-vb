using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyApiApp.Data;
using MyApiApp.Interfaces;
using MyApiApp.Models;
using StackExchange.Redis;
using System.Text.Json;


namespace MyApiApp.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly StackExchange.Redis.IDatabase _cache;


        public UsersRepository(ApplicationDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _cache = redis.GetDatabase();
        }

        public async Task<List<Users>> GetAllAsync()
        {
            string cacheKey = "users:all";

            var cachedUsers = await _cache.StringGetAsync(cacheKey);
            if (cachedUsers.HasValue)
            {
                return JsonSerializer.Deserialize<List<Users>>(cachedUsers)!;
            }

            var users = await _context.Users.ToListAsync();
            await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(users), TimeSpan.FromMinutes(10));
        
            return users;
        }

    }
}