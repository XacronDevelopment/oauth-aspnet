using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAuth.AspNet.Tokens;
using Microsoft.AspNetCore.Http;

namespace ResourceServer2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var jwtBearerOptions = new JwtBearerOptions
                                   {
                                       AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme,
                                       AutomaticAuthenticate = true
                                   };
            jwtBearerOptions.SecurityTokenValidators.Clear();
            jwtBearerOptions.SecurityTokenValidators.Add(new TicketDataFormatTokenValidator());

            app.UseJwtBearerAuthentication(jwtBearerOptions);

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

            app.Run(
                       async (context) =>
                       {
                           await context.Response.WriteAsync("Resource Server 2");
                       }
                   );
        }
    }
}
