using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Repositories
{
    public interface ILokacijaRepository
    {
        public Task<IEnumerable<LokacijaDto>> GetAll();

        public Task<int> Save(CreateLokacijaDto lokacija);

        public Task Delete(int id);

        public Task<ActionResult<LokacijaDto?>> GetById(int id);


    }
}
