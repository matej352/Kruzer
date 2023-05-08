namespace KruzerApp.DTOs
{
    public class CreatePutnikDto
    {
        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public string Nadimak { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Lozinka { get; set; } = null!;

        public char Spol { get; set; }
    }
}
