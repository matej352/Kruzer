using KruzerApp.DTOs;
using KruzerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
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

        public async Task<ActionResult<Putnik?>> GetById(int id)
        {
            var putnik = await _context.Putniks.FindAsync(id);

            return await Task.FromResult(putnik);
        }



        public async Task<int> Save(CreatePutnikDto putnik)
        {

            checkIsNadimakUnique(putnik.Nadimak);

            var id = await findLatestId();


            Putnik savedPutnik = new Putnik
            {
               Id = id.Value + 1,
               Ime = putnik.Ime,
               Prezime = putnik.Prezime,
               Nadimak = putnik.Nadimak,
               Email = putnik.Email,
               Lozinka = putnik.Lozinka,
               Spol = putnik.Spol
            };

            _context.Add(savedPutnik);
            await _context.SaveChangesAsync();
            return await Task.FromResult(savedPutnik.Id);
        }

        public async Task Update(UpdatePutnikDto putnikDto, String nadimak)
        {

            checkIsNewNadimakUnique(putnikDto.Nadimak, nadimak);

            var putnik = await _context.Putniks.SingleOrDefaultAsync(p => p.Nadimak == nadimak);

            if(putnik is not null)
            {
                putnik.Ime = putnikDto.Ime;
                putnik.Prezime = putnikDto.Prezime;
                putnik.Nadimak = putnikDto.Nadimak;
                putnik.Email = putnikDto.Email;
                await _context.SaveChangesAsync();
            } else
            {
                throw new Exception($"Putnik with nadimak {nadimak} does not exists!");
            }
        }

        public async Task Delete(int id)
        {
            var putnik = await _context.Putniks.FindAsync(id);
            if (putnik is null)
            {
                throw new Exception($"Putnik with id {id} does not exists!");
            }
            _context.Remove(putnik);

            await _context.SaveChangesAsync();
        }




        public async Task<ActionResult<int>> findLatestId()
        {
            var id = _context.Putniks.Max(p => p.Id);
            return await Task.FromResult(id);
        }

        public void checkIsNadimakUnique(string nadimak)
        {
            Putnik? putnik = _context.Putniks.Where(p => p.Nadimak == nadimak).SingleOrDefault(); 

            if (putnik != null)
            {
                throw new Exception("Nadimak should be unique!");
            }
        }

        public void checkIsNewNadimakUnique(string nadimakInDto, string nadimakInDatabase)
        {
            if (nadimakInDto != nadimakInDatabase)
            {
                Putnik? putnik = _context.Putniks.Where(p => p.Nadimak == nadimakInDto).SingleOrDefault();

                if (putnik != null)
                {
                    throw new Exception("New nadimak should be unique!");
                }
            }
        }


    }
}
