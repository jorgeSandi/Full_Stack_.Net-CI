using FullStackCI.Models;
using FullStackCI.Services;
using Moq;

namespace FullStackCITest.Services
{
    public class BookServiceTest
    {
        private readonly Mock<IUnitOfWorkService> _unitOfWorkService;
        private readonly BookService _service;

        public BookServiceTest()
        {
            _unitOfWorkService = new Mock<IUnitOfWorkService>();
            _service = new BookService(this._unitOfWorkService.Object);
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnsAllBooksAsync()
        {
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Book 1", AuthorId = 1, CategoryId = 1, PublicationYear = 2025 },
                new() { Id = 2, Title = "Book 2", AuthorId = 1, CategoryId = 1, PublicationYear = 2025 }
            };
            _unitOfWorkService.Setup(u => u.BookRepository.GetAllAsync()).ReturnsAsync(books);

            var result = await _service.GetAllBooksAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Book 1", result.First().Title);
        }
    }
}