using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreAPIPostgreSQL.Model;
using NetCoreAPIPostgreSQL.Model.Filters;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public interface ILocalidadRepositories
    {
        Task<IEnumerable<Localidad>> GetAllLocalidad(LocalidadesFilters filters);
        Task<Localidad> GetLocalidad(int id);
        Task<Localidad> GetLocalidadByName(string name);
        Task<int> InsertDefaultLocalidad(Localidad localidad);
        Task<bool> UpdateLocalidad(Localidad localidad);
        Task<bool> DeleteLocalidad(int id);
    }
}