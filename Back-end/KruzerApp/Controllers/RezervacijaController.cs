using KruzerApp.DTOs;
using KruzerApp.Models;
using KruzerApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijaController : ControllerBase
    {
        private readonly IRezervacijaRepository _repository;

        public RezervacijaController(IRezervacijaRepository repository)
        {
            _repository = repository;
        }

        /*

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Putnik>> Get()
        {
            var rezervacije = await _repository.GetAll();

            return rezervacije;

        }

        */

        [HttpGet]
        [Route("GetOne")]
        public async Task<ActionResult<RezervacijaDto>> GetRezervacija([FromQuery(Name = "param")] int id)
        {
            var rezervacija = await _repository.GetById(id);
            if (rezervacija.Value == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: $"Invalid id = {id}");
            }
            return rezervacija.Value;

        }

        

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<RezervacijaDto>> CreateRezervacija(CreateRezervacijaDto newRezervacija)
        {
            try
            {
                int newRezervacijaId = await _repository.Save(newRezervacija);

                RezervacijaDto? rezervacija = (await _repository.GetById(newRezervacijaId)).Value;

                return CreatedAtAction(nameof(GetRezervacija), new { id = newRezervacijaId }, rezervacija);
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRezervacija(int id, UpdateRezervacijaDto rezervacija)
        {
            try
            {
                await _repository.Update(rezervacija, id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRezervacija(int id)
        {
            try
            {
                await _repository.Delete(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }

    }
}
