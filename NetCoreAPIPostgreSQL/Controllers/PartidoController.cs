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

    public class PartidoController : Controller
    {
        private readonly IPartidoRepositories _partidoRepositories;
        public PartidoController(IPartidoRepositories partidoRepositories)
        {
            _partidoRepositories = partidoRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPartido()
        {
            return Ok(await _partidoRepositories.GetAllPartidos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartido(int id)
        {
            return Ok(await _partidoRepositories.GetPartido(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartido([FromBody] Partido partido)
        {
            if (partido == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _partidoRepositories.InsertDefaultPartido(partido);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePartido([FromBody] Partido partido)
        {
            if (partido == null)
                return BadRequest();

            await _partidoRepositories.UpdatePartido(partido);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartido(int id)
        {

            await _partidoRepositories.DeletePartido(id);

            return NoContent();
        }

    }
}