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
        private HttpClient client = new HttpClient();
        private readonly IPartidoRepositories _partidoRepositories;
        private readonly IProvinciaRepositories _provinciaRepositories;
        public PartidoController(IPartidoRepositories partidoRepositories, IProvinciaRepositories provinciaRepositories)
        {
            _partidoRepositories = partidoRepositories;
            _provinciaRepositories = provinciaRepositories;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPartido()
        {
            return Ok(await _partidoRepositories.GetAllPartidos());
        }

        [HttpGet("getdatagobtodb")]
        public async Task<IActionResult> GetDataGobProvincias()
        {
            var datos = await GetPartidoscDataGobAsyn();
            Partido partidos = new Partido();
            Provincia provincias = new Provincia();


            foreach (PartidoViewModels item in datos.municipios)
            {
                partidos.nombre = item.nombre;
                //Pais 1 Argentina
                // partidos.idprovincia = 1;

                try
                {
                    dynamic validate = await _partidoRepositories.GetPartidoByName(partidos.nombre);
                    if (validate == null)
                    {
                        provincias = await _provinciaRepositories.GetProvinciaByName(item.provincia.nombre);

                        if (provincias != null)
                        {
                            partidos.idprovincia = provincias.id;
                            await _partidoRepositories.InsertDefaultPartido(partidos);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }


            return Ok(datos);
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


        private async Task<ResponsePartido> GetPartidoscDataGobAsyn()
        {
            ResponsePartido product = null;
            HttpResponseMessage response = await client.GetAsync("https://apis.datos.gob.ar/georef/api/municipios?campos=id,nombre,provincia.nombre,provincia.id&max=2000");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<ResponsePartido>();

                return product;
            }

            return null;
        }

    }
}