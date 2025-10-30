namespace FullStackCI.Dtos
{
    public class AuthResponseDTO
    {
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
        public string? Username { get; set; }
    }
}