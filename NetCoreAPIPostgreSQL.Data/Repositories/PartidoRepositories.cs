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
    public class PartidoRepositories : IPartidoRepositories
    {
        private PostgreSQLConfiguration _connectionString;

        public PartidoRepositories(PostgreSQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        //Eliminar
        public async Task<bool> DeletePartido(int id)
        {
            var db = dbConnection();

            var sql = @"
                DELETE FROM public.partidos  WHERE id=@Id
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = id});

            return result > 0;
        }

        public async Task<IEnumerable<Partido>> GetAllPartidos()
        {

            var db = dbConnection();
            var query = @"
                SELECT  id, nombre, idprovincia  From public.partidos
                            ";

            return await db.QueryAsync<Partido>(query, new { });

        }

        public async Task<Partido> GetPartido(int id)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idprovincia FROM public.partidos
                         WHERE id=@Id   ";


            return await db.QueryFirstOrDefaultAsync<Partido>(query, new { Id = id});
        }

        public async Task<bool> InsertDefaultPartido(Partido partido)
        {
            var db = dbConnection();

            var sql = @"
                 INSERT INTO public.partidos( nombre, idprovincia)
                 VALUES (  @Nombre, @idProvincia)                
                            ";

            var result = await db.ExecuteAsync(sql, new { partido.nombre, partido.idprovincia});

            return result > 0;
        }

        //Guardar
        public async Task<bool> UpdatePartido(Partido partido)
        {
            var db = dbConnection();


            var sql = @"
             UPDATE public.partidos
             SET nombre=@Nombre  
             WHERE id=@Id
                            ";


            var result = await db.ExecuteAsync(sql, new { Nombre = partido.nombre, Id = partido.id});

            return result > 0;
        }

        public async Task<Partido> GetPartidoByName(string name)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idprovincia FROM public.partidos
                         WHERE nombre=@Name   ";


            return await db.QueryFirstOrDefaultAsync<Partido>(query, new { Name = name });
        }
    }
}
