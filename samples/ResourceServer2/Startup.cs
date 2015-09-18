using System.Collections.Generic;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Authentication.OAuthBearer;
using System.IdentityModel.Tokens;
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
            app.UseOAuthBearerAuthentication(
                                                options =>
                                                {
                                                    options.AuthenticationScheme = OAuthBearerAuthenticationDefaults.AuthenticationScheme;
                                                    options.AutomaticAuthentication = true;
                                                    options.SecurityTokenValidators = new List<ISecurityTokenValidator>() { new TicketDataFormatTokenValidator() };
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
