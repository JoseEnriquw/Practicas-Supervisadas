using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Model
{
    public class Provincia
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idpais { get; set; }

    }
}
