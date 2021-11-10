using Dapper;
using NetCoreAPIPostgreSQL.Model;
using NetCoreAPIPostgreSQL.Model.Filters;
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
        public async Task<IEnumerable<Localidad>> GetAllLocalidad(LocalidadesFilters filters)
        {

            var db = dbConnection();
            string query;

            if (filters.nombrePartido != "" && filters.nombreLocalidad != "")
            {

                query = @"
                SELECT  lo.id, lo.nombre, lo.idpartido From public.localidades as lo
                inner join partidos as part on part.id = lo.idpartido
                        where part.nombre ilike @PartNombre and lo.nombre ILIKE  @LocNombre
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Localidad>(query, new { PartNombre = filters.nombrePartido, LocNombre = "%" + filters.nombreLocalidad + "%" });
            }
            else if (filters.nombrePartido != "")
            {

                query = @"
                SELECT  lo.id, lo.nombre, lo.idpartido From public.localidades as lo
                inner join partidos as part on part.id = lo.idpartido
                        where part.nombre ilike @PartNombre
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Localidad>(query, new { PartNombre = filters.nombrePartido });
            }
            else if (filters.nombreLocalidad != "")
            {

                query = @"
                SELECT  lo.id, lo.nombre, lo.idpartido From public.localidades as lo
                inner join partidos as part on part.id = lo.idpartido
                        where lo.nombre ILIKE  @LocNombre
                        ORDER BY part.id ASC
                            ";

                return await db.QueryAsync<Localidad>(query, new { LocNombre = "%" + filters.nombreLocalidad + "%" });
            }
            else
            {
                query = @"
                SELECT  id, nombre, idpartido From public.localidades
                order by id asc
                            ";
                var localidad = await db.QueryAsync<Localidad>(query, new { });

                return localidad;
            }
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
        public async Task<int> InsertDefaultLocalidad(Localidad localidad)
        {
            var db = dbConnection();

            var sql = @"
                 INSERT INTO public.localidades( nombre, idpartido)
                 VALUES (  @Nombre, @idPartido)
                        ";

            var result = await db.ExecuteAsync(sql, new { localidad.nombre, localidad.idpartido});

            return result ;
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