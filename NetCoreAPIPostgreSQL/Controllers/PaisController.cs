using Microsoft.AspNetCore.Mvc;
using NetCoreAPIPostgreSQL.Data.Repositories;
using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : Controller
    {     
        private readonly IPaisRepositories _paisRepositories;
        public PaisController(IPaisRepositories paisRepositories)
        {
            _paisRepositories = paisRepositories;
        }
        
        [HttpGet]
        public IActionResult GetAllPais()
        {
            return Ok( _paisRepositories.GetAllPais());
        }

        [HttpGet("{id}")]
        public IActionResult GetPais(int id)
        {
            return Ok( _paisRepositories.GetPais(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePais([FromBody] Pais pais)
        {
            if (pais == null) BadRequest();
             
            if(!ModelState.IsValid) BadRequest(ModelState);

            var created=await _paisRepositories.InsertDefaultPais(pais);

            return Created("created", created);
        }

        [HttpPost]
        public async Task<IActionResult> MigrarPais([FromBody] Pais pais)
        {
            if (pais == null) BadRequest();

            if (!ModelState.IsValid) BadRequest(ModelState);

            var created = await _paisRepositories.InsertAPIPais(pais);

            return Created("created", created);
        }


        [HttpPut]
        public async Task<IActionResult> UpdatePais([FromBody] Pais pais)
        {
            if (pais == null) BadRequest();

           await _paisRepositories.UpdatePais(pais);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais( int id)
        {
                   
            await _paisRepositories.DeletePais(id);

            return NoContent();
        }

    }    
    
}
