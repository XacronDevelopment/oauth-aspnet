using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using OAuth.AspNet.Tokens;

namespace ResourceServer2
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseJwtBearerAuthentication(
                                                options =>
                                                {
                                                    options.AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
                                                    options.AutomaticAuthentication = true;
                                                    options.SecurityTokenValidators.Clear();
                                                    options.SecurityTokenValidators.Add(new TicketDataFormatTokenValidator());
                                                }
                                            );

            app.UseMvc(
                          routes =>
                          {
                              routes.MapRoute(
                                                 "WebApi",
                                                 "api/{controller}/{action}/{id?}",
                                                 new { action = "Get" }
                                             );

                          }
                      );
        }
    }
}
