using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreAPIPostgreSQL.Model;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public interface ILocalidadRepositories
    {
        Task<IEnumerable<Localidad>> GetAllLocalidad();
        Task<Localidad> GetLocalidad(int id);
        Task<bool> InsertDefaultLocalidad(Localidad localidad);
        Task<bool> UpdateLocalidad(Localidad localidad);
        Task<bool> DeleteLocalidad(int id);
    }
}