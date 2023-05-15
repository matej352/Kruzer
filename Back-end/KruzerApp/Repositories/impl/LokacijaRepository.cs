using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KruzerApp.Repositories.impl
{
    public class LokacijaRepository : ILokacijaRepository
    {

        private readonly KruzerContext _context;

        public LokacijaRepository(KruzerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LokacijaDto>> GetAll()
        {

            var lokacije = await _context.Lokacijas.ToListAsync();

            var lokacijaDtos = lokacije.Select(l => new LokacijaDto
            {
                Id = l.Id,
                Grad = l.Grad,
                Država = l.Država
            });

            return lokacijaDtos;
        }

        public async Task<ActionResult<LokacijaDto?>> GetById(int id)
        {
            var lokacija = await _context.Lokacijas.FindAsync(id);

            if (lokacija is null)
            {
                throw new Exception($"Lokacija with id {id} does not exists!");
            }

            var lokacijaDto = new LokacijaDto
            {
                Id = lokacija.Id,
                Grad = lokacija.Grad,
                Država = lokacija.Država
            };

            return lokacijaDto;
        }



        public async Task<int> Save(CreateLokacijaDto lokacija)
        {

            var id = await findLatestId();

            Lokacija savedLokacija = new Lokacija
            {
                Id = id.Value + 1,
                Grad= lokacija.Grad,
                Država=lokacija.Država
            };

            _context.Add(savedLokacija);
            await _context.SaveChangesAsync();
            return await Task.FromResult(savedLokacija.Id);
        }


        public async Task Delete(int id)
        {
            var lokacija = await _context.Lokacijas.FindAsync(id);
            if (lokacija is null)
            {
                throw new Exception($"Lokacija with id {id} does not exists!");
            }
            _context.Remove(lokacija);

            await _context.SaveChangesAsync();
        }




        public async Task<ActionResult<int>> findLatestId()
        {
            var id = _context.Lokacijas.Max(p => p.Id);
            return await Task.FromResult(id);
        }


    }
}
