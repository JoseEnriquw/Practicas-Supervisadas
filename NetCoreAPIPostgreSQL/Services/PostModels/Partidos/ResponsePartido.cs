using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels
{
    public class ResponsePartido
    {
        public int cantidad { get; set; }
        public int inicio { get; set; }
        public List<PartidoViewModels> municipios { get; set; }
        public Parametros parametros { get; set; }
        public int total { get; set; }
    }
}
