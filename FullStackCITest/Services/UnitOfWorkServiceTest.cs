using FullStackCI.Data;
using FullStackCI.Models;
using FullStackCI.Services;
using Microsoft.EntityFrameworkCore;

namespace FullStackCITest.Services
{
    public class UnitOfWorkServiceTest
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWorkService _unitOfWorkService;

        public UnitOfWorkServiceTest()
        {
            // Usar base de datos en memoria para tests
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LibraryDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _unitOfWorkService = new UnitOfWorkService(_context);
        }
        [Fact]
        public async Task AddAsync_ValidAuthor_AddsToDatabase()
        {
            var author = new Author()
            {
                Name = "Test",
                Nationality = "Test",
                BirthYear = 2025
            };

            var result = _unitOfWorkService.AuthorCommandRepository.Create(author);
            await _unitOfWorkService.SaveChangesAsync();

            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingBook_ReturnsBook()
        {
            var _authorCreated = _unitOfWorkService.AuthorCommandRepository.Create(
                new Author()
                {
                    Name = "Test",
                    Nationality = "Test",
                    BirthYear = 2025
                });

            var _categoryCreated = _unitOfWorkService.CategoryCommandRepository.Create(new Category
            {
                Name = "Test",
                Description = "Test"
            });

            // Arrange
            var book = new Book { Id = 3, Title = "Test Book", AuthorId = _authorCreated.Id, CategoryId = _categoryCreated.Id };
            _unitOfWorkService.BookCommandRepository.Create(book);
            await _unitOfWorkService.SaveChangesAsync();

            // Act
            var resultBook = await _unitOfWorkService.BookRepository.GetByIdAsync(book.Id);
            // Assert
            Assert.Equal(book.Id, resultBook.Id);
            Assert.Equal("Test Book", resultBook.Title);
        }
        [Fact]
        public async Task GetByIdAsync_NonExistingBook_ReturnsNull()
        {
            // Act
            var result = await _unitOfWorkService.BookRepository.GetByIdAsync(2);
            // Assert
            Assert.Null(result);
            //result.Should().BeNull();
        }
        [Fact]
        public async Task AddAsync_ValidBook_AddsToDatabase()
        {
            // Arrange
            var book = new Book { Id = 2, Title = "New Book" };
            // Act
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            // Assert
            var result = await _context.Books.FindAsync(book.Id);

            Assert.NotNull(result);
            Assert.Equal("New Book", result.Title);
            Assert.Equal(2, result.Id);
        }
    }
}