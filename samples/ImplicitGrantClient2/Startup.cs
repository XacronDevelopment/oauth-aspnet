using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ImplicitGrantClient2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(
                          routes =>
                          {
                              routes.MapRoute(
                                                 "WebApi",
                                                 "{controller}/{action}/{id?}",
                                                 new { controller = "Home", action = "Index" }
                                             );

                          }
                      );
        }
    }
}
