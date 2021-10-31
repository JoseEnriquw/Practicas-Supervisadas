using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels.Localidades
{
    public class LocalidadViewModels
    {
        public string id { get; set; }
        public Municipio municipio { get; set; }
        public string nombre { get; set; }
    }
}
