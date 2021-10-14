using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data
{
    public class PostgreSQLConfiguration
    {
        public string ConnectionString { get; set; }
        private NpgsqlConnection conexion;
        private NpgsqlCommand comando;
        private NpgsqlDataReader lector;

        //Inicia la conexion
        public PostgreSQLConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
            conexion = new NpgsqlConnection(connectionString);
            comando = new NpgsqlCommand();
        }
        

        //Setea la consulta
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //Ejecuta la lectura
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        //Cierra la conexion
        public void cerrarConexion()
        {
            if (lector != null) lector.Close(); conexion.Close();
        }

        //Retorna el lector
        public NpgsqlDataReader Lector { get { return lector; } }

        //Ejecuta la accion
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            conexion.Open();
            comando.ExecuteNonQuery();
        }

        public void agregarParametro(string nombre, Object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
    }
}
