using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);        
        Task<bool> ExistsAsync(int id);
    }
}