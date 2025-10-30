namespace FullStackCI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; } = string.Empty;

        // Relaciones
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public bool IsBestSeller { get; internal set; }
    }
}
