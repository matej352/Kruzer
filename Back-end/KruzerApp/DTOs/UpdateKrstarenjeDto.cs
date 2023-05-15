namespace KruzerApp.DTOs
{
    public class UpdateKrstarenjeDto
    {
        public int Id { get; set; }

        public string Naslov { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public DateTime Datumpocetak { get; set; }

        public DateTime Datumkraj { get; set; }

        public List<LokacijaDto>? Lokacije { get; set; }
    }
}
