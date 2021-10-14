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
        Task<bool> InsertDefaultPais(Pais provincia);
        Task<bool> InsertAPIPais(Pais provincia);
        Task<bool> UpdatePais(Pais provincia);
        Task<bool> DeletePais(int id);

    }
}
