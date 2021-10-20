using Microsoft.AspNetCore.Mvc;
using NetCoreAPIPostgreSQL.Data.Repositories;
using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NetCoreAPIPostgreSQL.Services.PostModels;
using Newtonsoft.Json;

namespace NetCoreAPIPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LocalidadController : Controller
    {
        private readonly ILocalidadRepositories _localidadRepositories;
        public LocalidadController(ILocalidadRepositories localidadRepositories)
        {
            _localidadRepositories = localidadRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocalidad()
        {
            return Ok(await _localidadRepositories.GetAllLocalidad());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocalidad(int id)
        {
            return Ok(await _localidadRepositories.GetLocalidad(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocalidad([FromBody] Localidad localidad)
        {
            if (localidad == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _localidadRepositories.InsertDefaultLocalidad(localidad);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocalidad([FromBody] Localidad localidad)
        {
            if (localidad == null)
                return BadRequest();

            await _localidadRepositories.UpdateLocalidad(localidad);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalidad(int id)
        {
            await _localidadRepositories.DeleteLocalidad(id);

            return NoContent();
        }
    }
}