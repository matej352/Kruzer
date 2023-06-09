﻿using KruzerApp.DTOs;
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
        [Route("GetAll")]
        public async Task<IEnumerable<Putnik>> GetAll()
        {
            var putnici = await _repository.GetAll();

            return putnici;

        }



        [HttpGet]
        [Route("GetOne")]
        public async Task<ActionResult<Putnik>> GetPutnik([FromQuery(Name = "param")] int id)
        {
            var putnik = await _repository.GetById(id);
            if (putnik.Value == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: $"Invalid id = {id}");
            }
            return putnik.Value;

        }


        [HttpPost]
        [Route("Create")]
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
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }

        [HttpPut("{nadimak}")]
        public async Task<ActionResult> UpdatePutnik(string nadimak, UpdatePutnikDto putnik)
        {
            try
            {
                await _repository.Update(putnik, nadimak);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePutnik(int id)
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
