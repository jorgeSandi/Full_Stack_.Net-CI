using FullStackCI.Data;
using FullStackCI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackCI.Repositories
{
    public class BookCommandRepository(ApplicationDbContext context) : IBookCommandRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Book Create(Book book)
        {
            _context.Books.Add(book);
            //await _context.SaveChangesAsync();
            return book;
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                //await _context.SaveChangesAsync();
            }
        }
    }
}