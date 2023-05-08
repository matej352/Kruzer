using KruzerApp.Models;

namespace KruzerApp.Repositories
{
    public interface IKrstarenjeRepository
    {
        public Task<IEnumerable<Krstarenje>> GetAll();
    }
}
