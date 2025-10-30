namespace FullStackCI.Dtos
{
    public class CreateAuthorDto
    {
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int BirthYear { get; set; }
    }
}
