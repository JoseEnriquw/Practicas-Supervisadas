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
using NetCoreAPIPostgreSQL.Services.PostModels.Localidades;
using NetCoreAPIPostgreSQL.Model.Filters;

namespace NetCoreAPIPostgreSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LocalidadController : Controller
    {
        private HttpClient client = new HttpClient();
        private readonly ILocalidadRepositories _localidadRepositories;
        private readonly IPartidoRepositories _partidoRepositories;
        public LocalidadController(ILocalidadRepositories localidadRepositories, IPartidoRepositories partidoRepositories)
        {
            _localidadRepositories = localidadRepositories;
            _partidoRepositories = partidoRepositories;
        }

        [Route("get")]
        [HttpPost]
        public async Task<IActionResult> GetAllPartido([FromBody] LocalidadesFilters filters)
        {
            if (filters.nombreLocalidad == null)
            {
                filters.nombreLocalidad = "";
            }
            if (filters.nombrePartido == null)
            {
                filters.nombrePartido = "";
            }

            return Ok(await _localidadRepositories.GetAllLocalidad(filters));
        }

        [HttpGet("getdatagobtodb")]
        public async Task<IActionResult> GetDataGobProvincias()
        {
            var datos = await GetLocalidadescDataGobAsyn();
            Localidad localidad = new Localidad();
            Partido partidos = new Partido();
            string mensaje = "";
            int aux = 0;

            foreach (LocalidadViewModels item in datos.localidades)
            {
                localidad.nombre = item.nombre;
              

                try
                {
                    dynamic validate = await _localidadRepositories.GetLocalidadByName(localidad.nombre);
                    if (validate == null)
                    {
                        partidos = await _partidoRepositories.GetPartidoByName(item.municipio.nombre);

                        if (partidos != null)
                        {
                            localidad.idpartido = partidos.id;
                            var cant=await _localidadRepositories.InsertDefaultLocalidad(localidad);
                            if (cant != null) aux += cant;                
                        }

                        
                    }
                }
                catch (Exception ex)
                {
                }
            }

            if (aux > 0) { mensaje = "SE MIGRARON CON EXITO " + aux + " DATOS DE LOCALIDADES DE LA API DEL GOBIERNO."; }
            else mensaje = "NO HUBO INGRESO DE DATOS, DEBIDO A QUE YA ESTAN EN LA BASE DE DATOS O NO HAY DATOS PARA MIGRAR";


            return Ok(mensaje);
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


        private async Task<ResponseLocalidad> GetLocalidadescDataGobAsyn()
        {
            ResponseLocalidad product = null;
            HttpResponseMessage response = await client.GetAsync("https://apis.datos.gob.ar/georef/api/localidades?&campos=id,nombre,municipio&max=4200");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<ResponseLocalidad>();

                return product;
            }

            return null;
        }

    }
}