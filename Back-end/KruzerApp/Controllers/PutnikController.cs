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

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
