using FullStackCI.Dtos;
using FullStackCI.Models;

namespace FullStackCI.Services
{
    public class BookService(IUnitOfWorkService unitOfWork) : IBookService
    {
        private readonly IUnitOfWorkService _unitOfWork = unitOfWork;

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync();
            return books.Select(ConvertToDto);
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            return book == null ? null : ConvertToDto(book);
        }

        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            // Verificar que el autor existe
            var authorExists = await _unitOfWork.AuthorRepository.ExistsAsync(createBookDto.AuthorId);
            if (!authorExists)
            {
                throw new ArgumentException("El autor especificado no existe");
            }

            // Verificar que la categoría existe
            var categoryExists = await _unitOfWork.CategoryRepository.ExistsAsync(createBookDto.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException("La categoría especificada no existe");
            }

            var book = new Book
            {
                Title = createBookDto.Title,
                ISBN = createBookDto.ISBN,
                PublicationYear = createBookDto.PublicationYear,
                Pages = createBookDto.Pages,
                Description = createBookDto.Description,
                CategoryId = createBookDto.CategoryId,
                AuthorId = createBookDto.AuthorId
            };

            var createdBook = _unitOfWork.BookCommandRepository.Create(book);
            await _unitOfWork.SaveChangesAsync();
            return ConvertToDto(createdBook);
        }

        public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
            if (book == null) return null;

            // Verificar que el autor existe
            var authorExists = await _unitOfWork.AuthorRepository.ExistsAsync(updateBookDto.AuthorId);
            if (!authorExists)
            {
                throw new ArgumentException("El autor especificado no existe");
            }

            // Verificar que la categoría existe
            var categoryExists = await _unitOfWork.CategoryRepository.ExistsAsync(updateBookDto.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException("La categoría especificada no existe");
            }

            book.Title = updateBookDto.Title;
            book.ISBN = updateBookDto.ISBN;
            book.PublicationYear = updateBookDto.PublicationYear;
            book.Pages = updateBookDto.Pages;
            book.Description = updateBookDto.Description;
            book.CategoryId = updateBookDto.CategoryId;
            book.AuthorId = updateBookDto.AuthorId;

            _unitOfWork.BookCommandRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
            return ConvertToDto(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            if (!await _unitOfWork.BookRepository.ExistsAsync(id))
                return false;

            await _unitOfWork.BookCommandRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private BookDto ConvertToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Pages = book.Pages,
                Description = book.Description,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.Name ?? string.Empty,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.Name ?? string.Empty
            };
        }
    }
}
