using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Services.PostModels
{
    public class Parametros
    {
        public List<string> campos { get; set; }
        public int max { get; set; }
    }
}
