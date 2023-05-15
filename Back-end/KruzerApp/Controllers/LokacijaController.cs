using KruzerApp.DTOs;
using KruzerApp.Models;
using KruzerApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KruzerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LokacijaController : ControllerBase
    {
        private readonly ILokacijaRepository _repository;

        public LokacijaController(ILokacijaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<LokacijaDto>> GetAll()
        {
            var lokacije = await _repository.GetAll();
            return lokacije;

        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<LokacijaDto>> CreateLokacija(CreateLokacijaDto newLokacija)
        {
            try
            {
                int newLokacijaId = await _repository.Save(newLokacija);

                LokacijaDto? lokacija = (await _repository.GetById(newLokacijaId)).Value;

                return Ok(lokacija);
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLokacija(int id)
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
