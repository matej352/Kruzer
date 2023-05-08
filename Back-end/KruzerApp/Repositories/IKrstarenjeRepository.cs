using KruzerApp.DTOs;
using KruzerApp.Models;

namespace KruzerApp.Repositories
{
    public interface IKrstarenjeRepository
    {
        public Task<IEnumerable<KrstarenjeWithRezervacijeDto>> GetAll();
    }
}
