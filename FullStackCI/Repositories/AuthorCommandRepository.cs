using FullStackCI.Data;
using FullStackCI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackCI.Repositories
{
    public class AuthorCommandRepository(ApplicationDbContext context) : IAuthorCommandRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Author Create(Author Author)
        {
            _context.Authors.Add(Author);
            //await _context.SaveChangesAsync();
            return Author;
        }

        public void Update(Author Author)
        {
            _context.Entry(Author).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Author = await _context.Authors.FindAsync(id);
            if (Author != null)
            {
                _context.Authors.Remove(Author);
                //await _context.SaveChangesAsync();
            }
        }
    }
}