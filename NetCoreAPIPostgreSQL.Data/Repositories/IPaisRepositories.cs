using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCoreAPIPostgreSQL.Model;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public interface IPaisRepositories
    {
        Task<IEnumerable<Pais>> GetAllPais();
        Task<Pais> GetPais(int id);
        Task<bool> InsertDefaultPais(Pais pais);
        Task<bool> UpdatePais(Pais pais);
        Task<bool> DeletePais(int id);

    }
}
