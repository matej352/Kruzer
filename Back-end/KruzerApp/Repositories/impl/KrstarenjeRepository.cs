using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task ChangePopunjenost(int id, int count, string action = "add")
        {

            var krstarenje = await _context.Krstarenjes.FindAsync(id);
            if (krstarenje is null)
            {
                throw new Exception($"Krstarenje with id {id} does not exists!");
            }
            if (action == "add")
            {
                krstarenje.Popunjenost = krstarenje.Popunjenost + count;  
            } else
            {
                krstarenje.Popunjenost = krstarenje.Popunjenost - count;
            }
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<KrstarenjeWithRezervacijeDto>> GetAll()
        {

            var result = await _context.Krstarenjes
                             .Include(k => k.Rezervacijas)
                                .ThenInclude(r => r.Putnik)
                             .Include(k => k.Lokacijas)
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
                Lokacije = k.Lokacijas.Select(l => new LokacijaDto
                {
                    Id=l.Id,
                    Grad=l.Grad,
                    Država=l.Država
                }).ToList(),
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

        public async Task<KrstarenjeWithRezervacijeDto> Get(int id)
        {
            var krstarenje = await _context.Krstarenjes.Where(k => k.Id == id)
                .Include(k => k.Rezervacijas).ThenInclude(r => r.Putnik)
                .Include(k => k.Lokacijas).FirstOrDefaultAsync();

            if (krstarenje == null)
            {
                throw new Exception($"Krstarenje with id {id} does not exists!");
            }

            var krstarenjeDto = new KrstarenjeWithRezervacijeDto
            {
                Id = krstarenje.Id,
                Naslov = krstarenje.Naslov,
                Opis = krstarenje.Opis,
                Datumpocetak = krstarenje.Datumpocetak,
                Datumkraj = krstarenje.Datumkraj,
                Kapacitet = krstarenje.Kapacitet,
                Popunjenost = krstarenje.Popunjenost,
                Lokacije = krstarenje.Lokacijas.Select(l => new LokacijaDto
                {
                    Id = l.Id,
                    Grad = l.Grad,
                    Država = l.Država
                }).ToList(),
                Rezervacije = krstarenje.Rezervacijas.Select(r => new RezervacijaDto
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
            };

            return krstarenjeDto;
        }


        public async Task<ActionResult<Krstarenje?>> GetById(int id)
        {
            var krstarenje = await _context.Krstarenjes.FindAsync(id);

            return await Task.FromResult(krstarenje);
        }



        public async Task<int> Save(CreateKrstarenjeDto krstarenje)
        {

            var id = await findLatestId();

            List<int> lokacijaIds = new List<int>();

            if (krstarenje.Lokacije is not null)
            {
                foreach (LokacijaDto lokacijaDto in krstarenje.Lokacije)
                {
                    lokacijaIds.Add(lokacijaDto.Id);
                }
            }
      
            List<Lokacija> existingLokacije = await _context.Lokacijas
                .Where(l => lokacijaIds.Contains(l.Id))
                .ToListAsync();


            Krstarenje savedKrstarenje = new Krstarenje
            {
                Id = id.Value + 1,
                Naslov = krstarenje.Naslov,
                Opis = krstarenje.Opis,
                Datumpocetak = DateOnly.Parse(krstarenje.Datumpocetak.ToShortDateString()),
                Datumkraj = DateOnly.Parse(krstarenje.Datumkraj.ToShortDateString()),
                Kapacitet = krstarenje.Kapacitet,
                Popunjenost = krstarenje.Popunjenost,
                AdminId = 1,
                Lokacijas = existingLokacije
            };

            _context.Add(savedKrstarenje);
            await _context.SaveChangesAsync();

            return await Task.FromResult(savedKrstarenje.Id);
        }

        /*
        public async Task<int> Update(UpdateKrstarenjeDto krstarenje)
        {

            var krstarenjeInDb =  await _context.Krstarenjes.FindAsync(krstarenje.Id);

            if (krstarenjeInDb is null)
            {
                throw new Exception($"Krstarenje with id {krstarenje.Id} does not exists!");
            }

            List<int> lokacijaIds = new List<int>();

            if (krstarenje.Lokacije is not null)
            {
                foreach (LokacijaDto lokacijaDto in krstarenje.Lokacije)
                {
                    lokacijaIds.Add(lokacijaDto.Id);
                }
            }

            List<Lokacija> existingLokacije = await _context.Lokacijas
                .Where(l => lokacijaIds.Contains(l.Id))
                .ToListAsync();

            krstarenjeInDb.Naslov = krstarenje.Naslov;
            krstarenjeInDb.Opis = krstarenje.Opis;
            krstarenjeInDb.Datumpocetak = DateOnly.Parse(krstarenje.Datumpocetak.ToShortDateString());
            krstarenjeInDb.Datumkraj = DateOnly.Parse(krstarenje.Datumkraj.ToShortDateString());
            krstarenjeInDb.Lokacijas = existingLokacije;
            
            await _context.SaveChangesAsync();

            return await Task.FromResult(krstarenje.Id);

        } */

        public async Task<int> Update(UpdateKrstarenjeDto krstarenje)
        {
            var krstarenjeInDb = await _context.Krstarenjes
                .Include(k => k.Lokacijas)
                .FirstOrDefaultAsync(k => k.Id == krstarenje.Id);

            if (krstarenjeInDb is null)
            {
                throw new Exception($"Krstarenje with id {krstarenje.Id} does not exist!");
            }

            // Update the Krstarenje properties
            krstarenjeInDb.Naslov = krstarenje.Naslov;
            krstarenjeInDb.Opis = krstarenje.Opis;
            krstarenjeInDb.Datumpocetak = DateOnly.Parse(krstarenje.Datumpocetak.ToShortDateString());
            krstarenjeInDb.Datumkraj = DateOnly.Parse(krstarenje.Datumkraj.ToShortDateString());

            if (krstarenje.Lokacije is not null)
            {
                // Get the existing Lokacije connected to the Krstarenje
                var existingLokacijeIds = krstarenjeInDb.Lokacijas.Select(l => l.Id).ToList();

                // Get the Lokacije to be associated with the Krstarenje based on their IDs
                var selectedLokacijeIds = krstarenje.Lokacije.Select(l => l.Id).ToList();

                // Remove the Lokacije that are no longer selected
                var removedLokacije = krstarenjeInDb.Lokacijas
                    .Where(l => !selectedLokacijeIds.Contains(l.Id))
                    .ToList();
                foreach (var removedLokacija in removedLokacije)
                {
                    krstarenjeInDb.Lokacijas.Remove(removedLokacija);
                }

                // Add the newly selected Lokacije
                var addedLokacijeIds = selectedLokacijeIds.Except(existingLokacijeIds).ToList();
                var addedLokacije = await _context.Lokacijas
                    .Where(l => addedLokacijeIds.Contains(l.Id))
                    .ToListAsync();

                foreach (var addedLokacija in addedLokacije)
                {
                    krstarenjeInDb.Lokacijas.Add(addedLokacija);
                }
            }
            else
            {
                // If no Lokacije are provided, remove all existing Lokacije connected to the Krstarenje
                krstarenjeInDb.Lokacijas.Clear();
            }

            await _context.SaveChangesAsync();

            return krstarenje.Id;
        }



        public async Task<ActionResult<int>> findLatestId()
        {
            var id = _context.Krstarenjes.Max(p => p.Id);
            return await Task.FromResult(id);
        }

    }
}
