using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Model
{
    public class Localidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idpais { get; set; }
        public int idprovincia { get; set; }
        public int idpartido { get; set; }
    }
}