using KruzerApp.DTOs;
using KruzerApp.Models;
using KruzerApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KrstarenjeController : ControllerBase
    {

        private readonly IKrstarenjeRepository _repository;

        public KrstarenjeController(IKrstarenjeRepository repository)
        {
            _repository = repository;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<KrstarenjeWithRezervacijeDto>> Get()
        {
            var krstarenja = await _repository.GetAll();

            return krstarenja;

        }

    }
}
