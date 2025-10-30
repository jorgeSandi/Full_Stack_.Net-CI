namespace FullStackCI.Dtos
{
    /// <summary>
    /// Clase con información del autor
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// Identificador del Autor
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del autor
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Nacionalidad del autor
        /// </summary>
        public string Nationality { get; set; } = string.Empty;
        /// <summary>
        /// Fecha de nacimiento del autor
        /// </summary>
        public int BirthYear { get; set; }
    }
}
