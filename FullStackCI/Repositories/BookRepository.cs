using FullStackCI.Models;
using Microsoft.EntityFrameworkCore;
using FullStackCI.Data;

namespace FullStackCI.Repositories
{
    public class BookRepository (ApplicationDbContext context) : IBookRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
}
