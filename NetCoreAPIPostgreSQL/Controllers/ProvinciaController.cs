using Microsoft.AspNetCore.Mvc;
using NetCoreAPIPostgreSQL.Data.Repositories;
using NetCoreAPIPostgreSQL.Model;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NetCoreAPIPostgreSQL.Services.PostModels;
using Newtonsoft.Json;

using NetCoreAPIPostgreSQL.Model.Filters;

namespace NetCoreAPIPostgreSQL.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : Controller
    {
        private HttpClient client = new HttpClient();       


        private readonly IProvinciaRepositories _provinciaRepositories;
        private readonly IPaisRepositories _paisRepositories;

        public ProvinciaController(IProvinciaRepositories provinciaRepositories, IPaisRepositories paisRepositories)
        {
            _provinciaRepositories = provinciaRepositories;
           _paisRepositories=paisRepositories;
        }


        [HttpGet("getdatagobtodb")]
        public async Task<IActionResult> GetDataGobProvincias()
        {
             var datos = await GetProvinciacDataGobAsyn();
            Provincia provincias = new Provincia();
            string mensaje = "";
            int aux = 0;

            foreach (ProvinciaViewModels item in datos.provincias)
            {

                try
                {
                    dynamic validate = await _provinciaRepositories.GetProvinciaByName(item.nombre);

                    if (validate == null)
                    {
                        provincias.nombre = item.nombre;
                        //Pais  Argentina
                        Pais auxpais =await _paisRepositories.GetPaisByName("Argentina");
                        if (auxpais == null) {
                            Pais pais = new Pais();
                            pais.id = 0;
                            pais.nombre = "Argentina";
                            await _paisRepositories.InsertDefaultPais(pais);
                            provincias.idpais =_paisRepositories.GetPaisByName("Argentina").Result.id;
                        }
                        else{
                            provincias.idpais = auxpais.id;
                        }
                               
                        

                       dynamic cant= await _provinciaRepositories.InsertDefaultProvincia(provincias);
                        if (cant != null) aux += cant;
                    }
                }
                catch (Exception ex) {
                }
            }

            if (aux > 0) { mensaje = "SE MIGRARON CON EXITO " + aux + " DATOS DE PROVINCIAS DE LA API DEL GOBIERNO."; }
            else mensaje = "NO HUBO INGRESO DE DATOS, DEBIDO A QUE YA ESTAN EN LA BASE DE DATOS O NO HAY DATOS PARA MIGRAR";
            

            return Ok( mensaje);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProvincia(int id)
        {
            return Ok(await _provinciaRepositories.GetProvincia(id));
        }
        [Route("get")]
        [HttpPost]
        public async Task<IActionResult> GetAllProvincia([FromBody] ProvinciaFilters filters)
        {

            if (filters.paisNombre == null) {

                filters.paisNombre = "";
            }

           

           // string jsonString = System.Text.Json.JsonSerializer.Serialize(finish);

            return Ok(await _provinciaRepositories.GetAllProvincias(filters));

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


         private async Task<ResponseProvincia> GetProvinciacDataGobAsyn()
        {
            ResponseProvincia product = null;
            HttpResponseMessage response = await client.GetAsync("https://apis.datos.gob.ar/georef/api/provincias");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<ResponseProvincia>();

                return product;
            }

            return null;
        }

    }



}
