using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KruzerApp.Repositories.impl
{
    public class KrstarenjeRepository : IKrstarenjeRepository
    {
        private readonly KruzerContext _context;

        public KrstarenjeRepository(KruzerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KrstarenjeWithRezervacijeDto>> GetAll()
        {

            var result = await _context.Krstarenjes
                             .Include(k => k.Rezervacijas)
                             .ThenInclude(r => r.Putnik)
                             .ToListAsync();

            var krstarenjeDtos = result.Select(k => new KrstarenjeWithRezervacijeDto
            {
                Id = k.Id,
                Naslov = k.Naslov,
                Opis = k.Opis,
                Datumpocetak = k.Datumpocetak,
                Datumkraj = k.Datumkraj,
                Kapacitet = k.Kapacitet,
                Popunjenost = k.Popunjenost,
                Rezervacije = k.Rezervacijas.Select(r => new RezervacijaDto
                {
                    Id = r.Id,
                    Vrijeme = r.Vrijeme,
                    Brojputnika = r.Brojputnika,
                    KrstarenjeId = r.KrstarenjeId,
                    Putnik = new PutnikDto
                    {
                        Id = r.Putnik.Id,
                        Ime = r.Putnik.Ime,
                        Prezime = r.Putnik.Prezime,
                        Nadimak = r.Putnik.Nadimak,
                        Email = r.Putnik.Email,
                        Spol = r.Putnik.Spol
                    }
                }).ToList()
            }).ToList();

            return krstarenjeDtos;
        }
    }
}
