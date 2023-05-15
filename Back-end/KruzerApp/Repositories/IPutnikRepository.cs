using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Repositories
{
    public interface IPutnikRepository
    {
        public Task<IEnumerable<Putnik>> GetAll();

        public Task<int> Save(CreatePutnikDto putnik);

        public Task Update(UpdatePutnikDto putnik, String nadimak);

        public Task Delete(int id);

        public Task<ActionResult<Putnik?>> GetById(int id);

    }
}
