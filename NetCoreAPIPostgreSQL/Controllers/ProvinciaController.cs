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
    public class ProvinciaController : Controller
    {
        private readonly IProvinciaRepositories _provinciaRepositories;
        public ProvinciaController(IProvinciaRepositories provinciaRepositories)
        {
            _provinciaRepositories = provinciaRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProvincia()
        {
            return Ok(await _provinciaRepositories.GetAllProvincias());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvincia(int id)
        {
            return Ok(await _provinciaRepositories.GetProvincia(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvincia([FromBody] Provincia provincia)
        {
            if (provincia == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _provinciaRepositories.InsertDefaultProvincia(provincia);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePais([FromBody] Provincia provincia)
        {
            if (provincia == null)
                return BadRequest();

            await _provinciaRepositories.UpdateProvincia(provincia);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvincia(int id)
        {

            await _provinciaRepositories.DeleteProvincia(id);

            return NoContent();
        }
    }
}
