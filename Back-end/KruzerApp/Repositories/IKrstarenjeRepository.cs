using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Repositories
{
    public interface IKrstarenjeRepository
    {
        public Task<IEnumerable<KrstarenjeWithRezervacijeDto>> GetAll();

        public Task<KrstarenjeWithRezervacijeDto> Get(int id);

        public Task ChangePopunjenost(int id, int count, string action = "add");

        public Task<int> Save(CreateKrstarenjeDto krstarenje);

        public Task<ActionResult<Krstarenje?>> GetById(int id);

        public Task<int> Update(UpdateKrstarenjeDto krstarenje);
    }
}
