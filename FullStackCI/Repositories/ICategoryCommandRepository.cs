using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface ICategoryCommandRepository
    {
        Category Create(Category category);
        void Update(Category category);
        Task DeleteAsync(int id);
    }
}