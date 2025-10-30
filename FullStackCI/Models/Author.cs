namespace FullStackCI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
