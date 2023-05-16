namespace KruzerApp.DTOs
{
    public class UpdateKrstarenjeDto
    {
        public int Id { get; set; }

        public string Naslov { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public DateOnly Datumpocetak { get; set; }

        public DateOnly Datumkraj { get; set; }

        public List<LokacijaDto>? Lokacije { get; set; }
    }
}
