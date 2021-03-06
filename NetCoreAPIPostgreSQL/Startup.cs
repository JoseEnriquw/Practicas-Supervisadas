using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetCoreAPIPostgreSQL.Data;
using NetCoreAPIPostgreSQL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAPIPostgreSQL
{
    public class Startup
    {
        private readonly string MyCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var postgreSQLConnectionConfiguration = new PostgreSQLConfiguration(Configuration.GetConnectionString("PostgreSQLConnection"));
            services.AddSingleton(postgreSQLConnectionConfiguration);

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCors, builder =>
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });

            services.AddScoped<IPaisRepositories, PaisRepositories>();
            services.AddScoped<IProvinciaRepositories, ProvinciaRepositories>();
            services.AddScoped<IPartidoRepositories, PartidoRepositories>();
            services.AddScoped<ILocalidadRepositories, LocalidadRepositories>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreAPIPostgreSQL", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCoreAPIPostgreSQL v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyCors);
            });
        }
    }
}
