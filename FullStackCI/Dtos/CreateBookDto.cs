namespace FullStackCI.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }


        public bool IsBestSeller { get; internal set; }

    }
}
