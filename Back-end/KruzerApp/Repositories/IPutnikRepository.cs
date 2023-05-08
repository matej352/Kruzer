using KruzerApp.Models;

namespace KruzerApp.Repositories
{
    public interface IPutnikRepository
    {
        public Task<IEnumerable<Putnik>> GetAll();
    }
}
