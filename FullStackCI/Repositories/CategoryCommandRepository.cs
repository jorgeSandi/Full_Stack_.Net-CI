using FullStackCI.Data;
using FullStackCI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackCI.Repositories
{
    public class CategoryCommandRepository (ApplicationDbContext context) : ICategoryCommandRepository
    {
        private readonly ApplicationDbContext _context = context;
        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            //await _context.SaveChangesAsync();
            return category;
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                //await _context.SaveChangesAsync();
            }
        }
    }
}