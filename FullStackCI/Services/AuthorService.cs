using FullStackCI.Dtos;
using FullStackCI.Models;

namespace FullStackCI.Services
{
    /// <summary>
    /// Servicio que manipula la información de autores
    /// </summary>
    /// <param name="unitOfWork">Inyecta la dependencia del servicio Unit Of Work</param>
    public class AuthorService(IUnitOfWorkService unitOfWork) : IAuthorService
    {
        private readonly IUnitOfWorkService _unitOfWork = unitOfWork;

        /// <summary>
        /// Obtiene la lista de todos los autores
        /// </summary>
        /// <returns>Lista de tipo AuthorDto</returns>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAsync();
            return authors.Select(ConvertToDto);
        }

        /// <summary>
        /// Obtiene un autor según id
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <returns>Objeto con la información del autor</returns>
        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            return author == null ? null : ConvertToDto(author);
        }
        /// <summary>
        /// Crea e inserta un autor en la base de datos
        /// </summary>
        /// <param name="createAuthorDto">Información del autor a crear</param>
        /// <returns>Retorna el autor creado con su información</returns>
        public async Task<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var author = new Author
            {
                Name = createAuthorDto.Name,
                Nationality = createAuthorDto.Nationality,
                BirthYear = createAuthorDto.BirthYear
            };

            var createdAuthor = _unitOfWork.AuthorCommandRepository.Create(author);
            await _unitOfWork.SaveChangesAsync();
            return ConvertToDto(createdAuthor);
        }
        /// <summary>
        /// Actualiza un autor existente
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <param name="updateAuthorDto">Información del autor a actualizar</param>
        /// <returns>Retorna el autor actualizar con su información</returns>
        public async Task<AuthorDto?> UpdateAuthorAsync(int id, CreateAuthorDto updateAuthorDto)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
            if (author == null) return null;

            author.Name = updateAuthorDto.Name;
            author.Nationality = updateAuthorDto.Nationality;
            author.BirthYear = updateAuthorDto.BirthYear;

            _unitOfWork.AuthorCommandRepository.Update(author);
            await _unitOfWork.SaveChangesAsync();
            return ConvertToDto(author);
        }
        /// <summary>
        /// Elimina el autor de la base de datos
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <returns>Falso o verdadero si el cambio se realiza</returns>
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            if (!await _unitOfWork.AuthorRepository.ExistsAsync(id))
                return false;

            await _unitOfWork.AuthorCommandRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private AuthorDto ConvertToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Nationality = author.Nationality,
                BirthYear = author.BirthYear
            };
        }
    }
}