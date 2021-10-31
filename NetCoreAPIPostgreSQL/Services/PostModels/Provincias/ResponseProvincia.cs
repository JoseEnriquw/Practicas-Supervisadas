using NetCoreAPIPostgreSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels
{
    public class ResponseProvincia
    {
        public int cantidad { get; set; }
        public int inicio { get; set; }
        public ParametrosProvincia parametros { get; set; }
        public List<ProvinciaViewModels> provincias { get; set; }
        public int total { get; set; }
    }
}
