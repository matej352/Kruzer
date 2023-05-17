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


        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<KrstarenjeWithRezervacijeDto>> GetKrstarenja()
        {
            var krstarenja = await _repository.GetAll();

            return krstarenja;

        }

        [HttpGet]
        [Route("GetOne")]
        public async Task<ActionResult<KrstarenjeWithRezervacijeDto>> GetKrstarenje([FromQuery(Name = "param")] int id)
        {
            KrstarenjeWithRezervacijeDto krstarenje = await _repository.Get(id);
            if (krstarenje == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: $"Invalid id = {id}");
            }
            return krstarenje;

        }



        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<KrstarenjeWithRezervacijeDto>> CreateKrstarenje(CreateKrstarenjeDto newKrstarenje)
        {
            try
            {
                int newKrstarenjeId = await _repository.Save(newKrstarenje);

                Krstarenje? krstarenje = (await _repository.GetById(newKrstarenjeId)).Value;

                KrstarenjeWithRezervacijeDto krstarenjeDto = await _repository.Get(krstarenje!.Id);

                return Ok(krstarenjeDto);
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateKrstarenje(int id, UpdateKrstarenjeDto krstarenje)
        {
            try
            {
                await _repository.Update(krstarenje);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }



    }
}
