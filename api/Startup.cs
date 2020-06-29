using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using core.InjectionDependency;
using System.IO;
using System.Security.Policy;
using Microsoft.OpenApi.Models;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AdicionaInjecaoDependencia(services);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Injeção de Dependência dinâmica .netcore",
                        Version = "v1",
                        Description = "Exemplo de injeção de dependência dinamica .netcore! Basta apenas anotar a classe de DI com o tipo de escopo para a mágica acontecer!",
                        Contact = new OpenApiContact
                        {
                            Name = "Lucas Amaral",
                            Email = "eng.lucasamaral@gmail.com",
                            Url = new Uri("https://github.com/engLucasAmaral/injecao-dependencia-dinamica-.netcore")
                        }
                    }
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/v1/swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ".Net core - Injeção de dependência dinâmica!");
            });
        }
        private void AdicionaInjecaoDependencia(IServiceCollection services)
        {

            var classesDI = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).
                Where(
                    t => t.IsClass &&
                    (t.GetCustomAttributes<LifeTimeAttribute>().Any())
                )?.ToList();

            foreach (var typeClass in classesDI)
            {
                var attr = typeClass.GetCustomAttribute<LifeTimeAttribute>();

                Type typeInterface = typeClass;
                if (attr.Interface != null)
                    typeInterface = attr.Interface;

                if (attr.GetType() == typeof(RequestScoped))
                {
                    services.AddScoped(typeInterface, typeClass);
                }
                else if (attr.GetType() == typeof(Transient))
                {
                    services.AddTransient(typeInterface, typeClass);
                }
                else if (attr.GetType() == typeof(Singleton))
                {
                    services.AddSingleton(typeInterface, typeClass);
                }
            }
        }
    }
}
