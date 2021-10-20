using Dapper;
using NetCoreAPIPostgreSQL.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public class ProvinciaRepositories : IProvinciaRepositories 
    {
        private PostgreSQLConfiguration _connectionString;

        public ProvinciaRepositories(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        //Eliminar
        public async Task<bool> DeleteProvincia(int id)
        {
            var db = dbConnection();

            var sql = @"
                DELETE FROM public.provincias  WHERE id=@Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        //Devuelve las Provincias
        public async Task<IEnumerable<Provincia>> GetAllProvincias()
        {
     
            var db = dbConnection();
            var query = @"
                SELECT  id, nombre, idpais  From public.provincias
                            ";

            return await db.QueryAsync<Provincia>(query, new { });

        }

        //Devuelve una Provincia
        public async Task<Provincia> GetProvincia(int id)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idpais FROM public.provincias
                         WHERE id=@Id   ";


            return await db.QueryFirstOrDefaultAsync<Provincia>(query, new { Id = id });
        }

        //Insertar Provincia
        public async Task<bool> InsertDefaultProvincia(Provincia provincia)
        {
            var db = dbConnection();

            var sql = @"
                 INSERT INTO public.provincias( nombre, idpais)
                 VALUES (  @Nombre, @idPais)                
                            ";

             var result = await db.ExecuteAsync(sql, new { provincia.nombre, provincia.idpais});

            return result > 0;
        }

        //Guardar
        public async Task<bool> UpdateProvincia(Provincia provincia)
        {
            var db = dbConnection();


            var sql = @"
             UPDATE public.provincias
             SET nombre=@Nombre  
             WHERE id=@Id
                            ";


            var result = await db.ExecuteAsync(sql, new { Nombre = provincia.nombre, Id = provincia.id });

            return result > 0;
        }
    }
}
