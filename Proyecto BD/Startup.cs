using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Proyecto_BD.Models;


namespace Proyecto_BD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método se llama por el runtime de ASP .NET para agregar servicios al contenedor de dependencias.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            // Configura la conexión a la base de datos utilizando una cadena de conexión definida en appsettings.json
            services.AddDbContext<BDContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // Este método se llama por el runtime de ASP .NET para configurar el pipeline de la aplicación.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            // Asegura que la base de datos y sus tablas han sido creadas
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BDContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}