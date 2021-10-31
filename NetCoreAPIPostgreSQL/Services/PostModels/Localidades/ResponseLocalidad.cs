using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels.Localidades
{
    public class ResponseLocalidad
    {
        public int cantidad { get; set; }
        public int inicio { get; set; }
        public List<LocalidadViewModels> localidades { get; set; }
        public Parametros parametros { get; set; }
        public int total { get; set; }
    }
}
