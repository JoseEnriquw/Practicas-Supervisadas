using Dapper;
using NetCoreAPIPostgreSQL.Model;
using NetCoreAPIPostgreSQL.Model.Filters;
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

        public async Task<IEnumerable<Partido>> GetAllPartidos(PartidoFilters filters)
        {

            var db = dbConnection();
            string query;

            if (filters.nombreProvincia != "" && filters.nombrePartido != "")
            {

                query = @"
                SELECT  part.id, part.nombre, part.idprovincia  From public.partidos as part
                inner join provincias as p on p.id = part.idprovincia
                        where p.nombre like @ProvNombre and part.nombre ILIKE  @PartNombre
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Partido>(query, new { ProvNombre=filters.nombreProvincia,PartNombre = "%" + filters.nombrePartido + "%" });
            }else if (filters.nombreProvincia != "")
            {

                query = @"
                SELECT  part.id, part.nombre, part.idprovincia  From public.partidos as part
                inner join provincias as p on p.id = part.idprovincia
                        where p.nombre like  @ProvNombre 
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Partido>(query, new { ProvNombre = filters.nombreProvincia });
            }
            else if (filters.nombrePartido != "")
            {

                query = @"
                SELECT  part.id, part.nombre, part.idprovincia  From public.partidos as part
                inner join provincias as p on p.id = part.idprovincia
                        where part.nombre ILIKE   @PartNombre
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Partido>(query, new { PartNombre = "%" + filters.nombrePartido + "%" });
            }
            else
            {
                query = @"
                SELECT  id, nombre, idprovincia  From public.partidos
                order by id asc
                            ";

                return await db.QueryAsync<Partido>(query, new { });
            }
        }

        public async Task<Partido> GetPartido(int id)
        {

            var db = dbConnection();
            var query = @"
                SELECT   id, nombre, idprovincia FROM public.partidos
                         WHERE id=@Id   ";


            return await db.QueryFirstOrDefaultAsync<Partido>(query, new { Id = id});
        }

        public async Task<int> InsertDefaultPartido(Partido partido)
        {
            var db = dbConnection();

            var sql = @"
                 INSERT INTO public.partidos( nombre, idprovincia)
                 VALUES (  @Nombre, @idProvincia)                
                            ";

            var result = await db.ExecuteAsync(sql, new { partido.nombre, partido.idprovincia});

            return result ;
        }

        //Guardar
        public async Task<bool> UpdatePartido(Partido partido)
        {
            var db = dbConnection();


            var sql = @"
             UPDATE public.partidos
             SET nombre=@Nombre,idprovincia=@idProvincia  
             WHERE id=@Id
                            ";


            var result = await db.ExecuteAsync(sql, new { Nombre = partido.nombre, idProvincia = partido.idprovincia, Id = partido.id});

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
