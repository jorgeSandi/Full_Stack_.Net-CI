using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}