using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.LiveReload;

namespace MyCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) { 
            #if DEBUG
            services.AddLiveReload();
            #endif

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifeTime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                

                // Aggiorniamo un file per notificare al LiveReload che deve aggiornare la pagina
                lifeTime.ApplicationStarted.Register(() => {
                    string filePath = Path.Combine(env.ContentRootPath, "wwwroot/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });
            }
            app.UseStaticFiles();

            #if DEBUG
                app.UseLiveReload();
            #endif

            // app.UseMvcWithDefaultRoute()
            app.UseMvc(routeBuilder => {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
