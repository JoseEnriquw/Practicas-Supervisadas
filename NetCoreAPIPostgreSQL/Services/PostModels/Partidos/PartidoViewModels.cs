using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels
{
    public class PartidoViewModels
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public ProvinciaViewModels provincia { get; set; }

    }
}
