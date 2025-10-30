using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface IAuthorCommandRepository
    {
        Author Create(Author category);
        void Update(Author category);
        Task DeleteAsync(int id);
    }
}
