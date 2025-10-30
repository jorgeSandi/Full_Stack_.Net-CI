using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface IBookCommandRepository
    {
        Book Create(Book book);
        void Update(Book book);
        Task DeleteAsync(int id);
    }
}
