using FullStackCI.Data;
using FullStackCI.Repositories;

namespace FullStackCI.Services
{
    public class UnitOfWorkService(ApplicationDbContext applicationDbContext) : IUnitOfWorkService
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        public ICategoryRepository CategoryRepository { get; set; } = new CategoryRepository(applicationDbContext);
        public ICategoryCommandRepository CategoryCommandRepository { get; set; } = new CategoryCommandRepository(applicationDbContext);
        public IAuthorRepository AuthorRepository { get; set; } = new AuthorRepository(applicationDbContext);
        public IAuthorCommandRepository AuthorCommandRepository { get; set; } = new AuthorCommandRepository(applicationDbContext);
        public IBookRepository BookRepository { get; set; } = new BookRepository(applicationDbContext);
        public IBookCommandRepository BookCommandRepository { get; set; } = new BookCommandRepository(applicationDbContext);

        public void Dispose()
        {            
            _applicationDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync() => await _applicationDbContext.SaveChangesAsync();
    }
}