using FullStackCI.Dtos;

namespace FullStackCI.Services
{
    /// <summary>
    /// Servicio que manipula la información de autores
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Obtiene la lista de todos los autores
        /// </summary>
        /// <returns>Lista de tipo AuthorDto</returns>
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        /// <summary>
        /// Obtiene un autor según id
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <returns>Objeto con la información del autor</returns>
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        /// <summary>
        /// Crea e inserta un autor en la base de datos
        /// </summary>
        /// <param name="createAuthorDto">Información del autor a crear</param>
        /// <returns>Retorna el autor creado con su información</returns>
        Task<AuthorDto> CreateAuthor(CreateAuthorDto createAuthorDto);
        /// <summary>
        /// Actualiza un autor existente
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <param name="updateAuthorDto">Información del autor a actualizar</param>
        /// <returns>Retorna el autor actualizar con su información</returns>
        Task<AuthorDto?> UpdateAuthorAsync(int id, CreateAuthorDto updateAuthorDto);
        /// <summary>
        /// Elimina el autor de la base de datos
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <returns>Falso o verdadero si el cambio se realiza</returns>
        Task<bool> DeleteAuthorAsync(int id);
    }
}
