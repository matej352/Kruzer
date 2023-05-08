using KruzerApp.Models;
using System.Security.Principal;

namespace KruzerApp.Repositories.impl
{
    public class PutnikRepository : IPutnikRepository
    {
        private readonly KruzerContext _context;

        public PutnikRepository(KruzerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Putnik>> GetAll()
        {

            var putnici = _context.Putniks;

            return await Task.FromResult(putnici);
        }
    }
}
