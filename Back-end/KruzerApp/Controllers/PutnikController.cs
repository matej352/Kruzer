using KruzerApp.DTOs;
using KruzerApp.Models;
using KruzerApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KruzerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PutnikController : ControllerBase
    {

        private readonly IPutnikRepository _repository;

        public PutnikController(IPutnikRepository repository)
        {
            _repository = repository;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<Putnik>> Get()
        {
            var putnici = await _repository.GetAll();

            return putnici;

        }



        [HttpGet]
        [Route("GetPutnik")]
        public async Task<ActionResult<Putnik>> GetPutnik([FromQuery(Name = "param")] int id)
        {
            var putnik = await _repository.GetById(id);
            if (putnik.Value == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: $"Invalid id = {id}");
            }
            return putnik.Value;

        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<PutnikDto>> CreatePutnik(CreatePutnikDto newPutnik)
        { 
            try
            {
                int newPutnikId = await _repository.Save(newPutnik);

                Putnik? putnik = (await _repository.GetById(newPutnikId)).Value;


                return CreatedAtAction(nameof(GetPutnik), new { id = newPutnikId }, putnik);
            }
            catch(Exception e) 
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: e.Message);
            }
        
            
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
