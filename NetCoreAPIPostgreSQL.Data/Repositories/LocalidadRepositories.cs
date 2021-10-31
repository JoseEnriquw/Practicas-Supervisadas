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
    public class LocalidadRepositories : ILocalidadRepositories
    {

        private PostgreSQLConfiguration _connectionString;

        public LocalidadRepositories(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        //Eliminar
        public async Task<bool> DeleteLocalidad(int id)
        {
            var db = dbConnection();

            var sql = @"
                DELETE FROM public.localidades  WHERE id=@Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = id});

            return result > 0;
        }

        //Devuelve todas las localidades
        public async Task<IEnumerable<Localidad>> GetAllLocalidad()
        {

            var db = dbConnection();
            var query = @"
                SELECT  id, nombre,idpartido  From public.localidades
                            ";

            return await db.QueryAsync<Localidad>(query, new { });
        }

        //Devuelve una localidad POR ID
        public async Task<Localidad> GetLocalidad(int id)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idpartido FROM public.localidades
                    WHERE id=@Id   
                            ";

            return await db.QueryFirstOrDefaultAsync<Localidad>(query, new { Id = id});
        }

        //Devuelve una localidad POR NOMBRE
        public async Task<Localidad> GetLocalidadByName(string name)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idpartido FROM public.localidades
                    WHERE nombre=@Name   
                            ";

            return await db.QueryFirstOrDefaultAsync<Localidad>(query, new { Name = name });
        }

        //Insertar
        public async Task<bool> InsertDefaultLocalidad(Localidad localidad)
        {
            var db = dbConnection();

            var sql = @"
                 INSERT INTO public.localidades( nombre, idpartido)
                 VALUES (  @Nombre, @idPartido)
                        ";

            var result = await db.ExecuteAsync(sql, new { localidad.nombre, localidad.idpartido});

            return result > 0;
        }

         //Guardar
        public async Task<bool> UpdateLocalidad(Localidad localidad)
        {
            var db = dbConnection();

            var sql = @"
             UPDATE public.localidades
             SET nombre=@Nombre  
             WHERE id=@Id
                       ";

            var result = await db.ExecuteAsync(sql, new { Nombre = localidad.nombre, Id = localidad.id});

            return result > 0;
        }
    }
}