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
using core.InectionDependency;
using System.IO;
using System.Security.Policy;

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
        }
       private void AdicionaInjecaoDependencia(IServiceCollection services)
        {
            var classesDI = new List<Type>();
            HashSet<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToHashSet();

            Assembly.GetExecutingAssembly().GetReferencedAssemblies().ToList().ForEach(assembly =>
            {
                assemblies.Add(Assembly.Load(assembly));
            });
            assemblies.ToList().ForEach(assembly =>
            {
                classesDI.AddRange(assembly.GetTypes().Where(type =>
               {
                   return type.GetCustomAttributes().Where(attr =>
                   {
                       return (attr.GetType()) switch
                       {
                           var typeDI when
                           typeDI == typeof(Transient) ||
                           typeDI == typeof(Singleton) ||
                           typeDI == typeof(RequestScoped) => true,
                           _ => false
                       };
                   }).ToList().Count > 0;
               }).ToList());
            });
            classesDI.ForEach(type =>
            {
                if (type.GetCustomAttributes().Any(attr => attr.GetType() == typeof(RequestScoped)))
                {
                    services.AddScoped(type);
                }
                else if (type.GetCustomAttributes().Any(attr => attr.GetType() == typeof(Transient)))
                {
                    services.AddTransient(type);
                }
                else if (type.GetCustomAttributes().Any(attr => attr.GetType() == typeof(Singleton)))
                {
                    services.AddSingleton(type);
                }
            });

        }
    
    }
}
