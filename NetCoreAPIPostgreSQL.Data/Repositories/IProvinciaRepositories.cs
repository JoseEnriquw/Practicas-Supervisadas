using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
   public interface IProvinciaRepositories
    {
        Task<IEnumerable<Provincia>> GetAllProvincias();
        Task<Provincia> GetProvincia(int id);
        Task<Provincia> GetProvinciaByName(string name);
        Task<bool> InsertDefaultProvincia(Provincia provincia);
        Task<bool> UpdateProvincia(Provincia provincia);
        Task<bool> DeleteProvincia(int id);

    }
}
