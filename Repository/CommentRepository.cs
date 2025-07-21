using Microsoft.EntityFrameworkCore;
using MyApiApp.Data;
using MyApiApp.Interfaces;
using MyApiApp.Models;

namespace MyApiApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comments> CreateAsync(Comments commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comments?> DeleteAsync(int id)
        {
            var exist = await _context.Comments.FindAsync(id);

            if (exist == null)
            {
                return null;
            }

            _context.Comments.Remove(exist);
            await _context.SaveChangesAsync();
            return exist;

        }

        public async Task<List<Comments>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comments?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comments?> UpdateAsync(int id, Comments commentModel)
        {
            var exist = await _context.Comments.FindAsync(id);

            if (exist == null)
            {
                return null;
            }

            exist.Title = commentModel.Title;
            exist.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return exist;
        }
    }
}