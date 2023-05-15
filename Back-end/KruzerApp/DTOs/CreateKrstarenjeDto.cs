namespace KruzerApp.DTOs
{
    public class CreateKrstarenjeDto
    {
        public string Naslov { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public DateTime Datumpocetak { get; set; }

        public DateTime Datumkraj { get; set; }

        public int Kapacitet { get; set; }

        public int Popunjenost { get; set; }

        public List<LokacijaDto>? Lokacije { get; set; }
    }
}
