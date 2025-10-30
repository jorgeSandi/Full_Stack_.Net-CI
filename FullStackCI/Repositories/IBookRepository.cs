using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}