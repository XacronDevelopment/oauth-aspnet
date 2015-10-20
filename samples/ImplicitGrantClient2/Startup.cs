using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

namespace ImplicitGrantClient2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

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
