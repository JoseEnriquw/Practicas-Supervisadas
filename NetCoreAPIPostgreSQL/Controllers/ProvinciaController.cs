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
    public class ProvinciaController : Controller
    {
        private HttpClient client = new HttpClient();       


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

        [HttpGet("getdatagobtodb")]
        public async Task<IActionResult> GetDataGobProvincias()
        {
            List<ProvinciaViewModels> datos = await GetProvinciacDataGobAsyn();
            Provincia provincias = new Provincia();

            foreach (ProvinciaViewModels item in datos)
            {
                provincias.nombre = item.nombre;
                //Pais 1 Argentina
                provincias.idpais = 1;

               await _provinciaRepositories.InsertDefaultProvincia(provincias);
            }

            

            return Ok( datos);
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


       private async Task<List<ProvinciaViewModels>> GetProvinciacDataGobAsyn()
        {
            List<ProvinciaViewModels> product = null;
            HttpResponseMessage response = await client.GetAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
            if (response.IsSuccessStatusCode)
            {
                product = JsonConvert.DeserializeObject<List<ProvinciaViewModels>> (await response.Content.ReadAsAsync<string>());
            }
            return product;
        }

    }



}
