using FullStackCI.Data;
using FullStackCI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackCI.Repositories
{
    public class AuthorRepository (ApplicationDbContext context) : IAuthorRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(c => c.Id == id);
        }
    }
}