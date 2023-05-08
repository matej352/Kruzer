namespace KruzerApp.DTOs
{
    public class KrstarenjeWithRezervacijeDto
    {
        public int Id { get; set; }

        public string Naslov { get; set; } = null!;

        public string Opis { get; set; } = null!;

        public DateOnly Datumpocetak { get; set; }

        public DateOnly Datumkraj { get; set; }

        public int Kapacitet { get; set; }

        public int Popunjenost { get; set; }

        public List<RezervacijaDto>? Rezervacije { get; set; }


    }

    public class RezervacijaDto
    {
        public int Id { get; set; }

        public DateOnly Vrijeme { get; set; }

        public int Brojputnika { get; set; }

        public int KrstarenjeId { get; set; }

        public PutnikDto? Putnik { get; set; }
    }

    public class PutnikDto
    {
        public int Id { get; set; }

        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public string Nadimak { get; set; } = null!;

        public string Email { get; set; } = null!;

        public char Spol { get; set; }
    }
}
