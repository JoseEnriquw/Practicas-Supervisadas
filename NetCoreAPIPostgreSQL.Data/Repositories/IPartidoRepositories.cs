using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public interface IPartidoRepositories
    {
        Task<IEnumerable<Partido>> GetAllPartidos();
        Task<Partido> GetPartido(int id);
        Task<Partido> GetPartidoByName(string name);
        Task<bool> InsertDefaultPartido(Partido partido);
        Task<bool> UpdatePartido(Partido partido);
        Task<bool> DeletePartido(int id);

    }
}
