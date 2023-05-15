using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Repositories
{
    public interface IRezervacijaRepository
    {
        public Task<int> Save(CreateRezervacijaDto rezervacija);

        public Task Update(UpdateRezervacijaDto rezervacija, int id);

        public Task Delete(int id);

        public Task<ActionResult<RezervacijaDto?>> GetById(int id);
    }
}
