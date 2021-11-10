using Dapper;
using NetCoreAPIPostgreSQL.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL.Data.Repositories
{
    public class PaisRepositories : IPaisRepositories
    {
        private PostgreSQLConfiguration _connectionString;

        public PaisRepositories(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        //DELETE
        public async Task<bool> DeletePais(int id)
        {
            var db = dbConnection();

            var sql = @"
                DELETE FROM public.pais WHERE id=@Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        //DEVUELVE TODAS LAS PAIS
        public async Task<IEnumerable<Pais>> GetAllPais()
        {

            var db = dbConnection();
            var query = @"
                SELECT  id, nombre  From public.pais
                            ";


            return await db.QueryAsync<Pais>(query, new { });

                      
        }

        //DEVUELVE UN PAIS
        public async Task<Pais> GetPais(int id)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre FROM public.pais
                         WHERE id=@Id   ";


          return await db.QueryFirstOrDefaultAsync<Pais>(query, new {Id=id });
        }

        //INSERT 
        public async Task<bool> InsertDefaultPais(Pais pais)
        {
            var db = dbConnection();
            
            var sql = @"
                 INSERT INTO public.pais( nombre)
                 VALUES (  @Nombre)                
                            ";



            var result = await db.ExecuteAsync(sql, new { pais.nombre });

            return result > 0;
        }

        //UPDATE
        public async Task<bool> UpdatePais(Pais pais)
        {
            var db = dbConnection();
            

            var sql = @"
             UPDATE public.pais
             SET nombre=@Nombre            
             WHERE id=@Id
                            ";


            var result = await db.ExecuteAsync(sql, new { Nombre=pais.nombre,Id=pais.id });

            return result > 0;
        }

        public async Task<Pais> GetPaisByName(string name)
        {
             var db = dbConnection();
            var query = @"
                SELECT   id, nombre FROM public.pais
                         WHERE nombre=@Name   ";


            return await db.QueryFirstOrDefaultAsync<Pais>(query, new { Name = name});
        }
    }
}
