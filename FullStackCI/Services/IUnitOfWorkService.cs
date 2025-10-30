using FullStackCI.Repositories;

namespace FullStackCI.Services
{
    public interface IUnitOfWorkService : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        ICategoryCommandRepository CategoryCommandRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IAuthorCommandRepository AuthorCommandRepository { get; }
        IBookRepository BookRepository { get; }
        IBookCommandRepository BookCommandRepository { get; }
        Task<int> SaveChangesAsync();
    }
}