using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KruzerApp.Repositories.impl
{
    public class RezervacijaRepository : IRezervacijaRepository
    {
        private readonly KruzerContext _context;
        private IKrstarenjeRepository _krstarenje;

        public RezervacijaRepository(KruzerContext context, IKrstarenjeRepository krstarenje)
        {
            _context = context;
            _krstarenje = krstarenje;
        }

        public async Task<ActionResult<RezervacijaDto?>> GetById(int id)
        {
            var rezervacija = await _context.Rezervacijas.Include(r => r.Putnik).FirstOrDefaultAsync(r => r.Id == id);

            if (rezervacija != null)
            {
                var rezervacijaDto = new RezervacijaDto
                {
                    Id = rezervacija.Id,
                    Vrijeme = rezervacija.Vrijeme,
                    Brojputnika = rezervacija.Brojputnika,
                    KrstarenjeId = rezervacija.KrstarenjeId,
                    Putnik = new PutnikDto
                    {
                        Id = rezervacija.Putnik.Id,
                        Ime = rezervacija.Putnik.Ime,
                        Prezime = rezervacija.Putnik.Prezime,
                        Email = rezervacija.Putnik.Email,
                        Nadimak = rezervacija.Putnik.Nadimak,
                        Spol = rezervacija.Putnik.Spol,
                    }
                };

                return await Task.FromResult(rezervacijaDto);
            }
            else
            {
                throw new Exception($"Rezervacija with id {id} does not exists!");
            }

        }



        public async Task<int> Save(CreateRezervacijaDto rezervacijaDto)
        {

            var id = await findLatestId();

            Rezervacija savedRezervacija = new Rezervacija
            {
                Id = id.Value + 1,
                Vrijeme = DateOnly.FromDateTime(DateTime.Today),
                Brojputnika = rezervacijaDto.Brojputnika,
                KrstarenjeId = rezervacijaDto.KrstarenjeId,
                PutnikId = rezervacijaDto.PutnikId,
            };

            _context.Add(savedRezervacija);
            await _context.SaveChangesAsync();

            await _krstarenje.ChangePopunjenost(rezervacijaDto.KrstarenjeId, rezervacijaDto.Brojputnika);

            return await Task.FromResult(savedRezervacija.Id);
        }

        public async Task Update(UpdateRezervacijaDto rezervacijaDto, int id)
        {
            var rezervacija = await _context.Rezervacijas.SingleOrDefaultAsync(r => r.Id == id);

            if (rezervacija is not null)
            {

                await _krstarenje.ChangePopunjenost(id, rezervacija.Brojputnika, "remove"); //sub brojputnika in old rezervacija
                await _krstarenje.ChangePopunjenost(id, rezervacijaDto.Brojputnika); //add brojputnika in new rezervacija

                rezervacija.Brojputnika = rezervacijaDto.Brojputnika;
                rezervacija.Vrijeme = DateOnly.FromDateTime(DateTime.Today);

                

                await _context.SaveChangesAsync();

               
            }
            else
            {
                throw new Exception($"Rezervacija with id {id} does not exists!");
            }
        }

        public async Task Delete(int id)
        {
            var rezervacija = await _context.Rezervacijas.FindAsync(id);
            if (rezervacija is null)
            {
                throw new Exception($"Rezervacija with id {id} does not exists!");
            }
            _context.Remove(rezervacija);

            await _context.SaveChangesAsync();

            await _krstarenje.ChangePopunjenost(id, rezervacija.Brojputnika, "remove");

        }


        public async Task<ActionResult<int>> findLatestId()
        {
            var id = _context.Rezervacijas.Max(p => p.Id);
            return await Task.FromResult(id);
        }


    }
}
