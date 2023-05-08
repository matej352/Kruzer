using KruzerApp.Models;

namespace KruzerApp.Repositories.impl
{
    public class KrstarenjeRepository : IKrstarenjeRepository
    {
        private readonly KruzerContext _context;

        public KrstarenjeRepository(KruzerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Krstarenje>> GetAll()
        {

            var krstarenja = _context.Krstarenjes;

            return await Task.FromResult(krstarenja);
        }
    }
}
