using Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAuth.AspNet.AuthServer;
using System;

namespace AuthorizationServer
{
    public partial class Startup
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

            // Enable Application Sign In Cookie            
            app.UseCookieAuthentication(
                                           new CookieAuthenticationOptions
                                           {
                                               AuthenticationScheme = "Application",
                                               AutomaticAuthenticate = false,
                                               LoginPath = new PathString(Paths.LoginPath),
                                               LogoutPath = new PathString(Paths.LogoutPath)
                                           }
                                       );

            app.UseCookieAuthentication(
                                           new CookieAuthenticationOptions
                                           {
                                               AuthenticationScheme = "External",
                                               AutomaticAuthenticate = false,
                                               CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
                                               ExpireTimeSpan = TimeSpan.FromMinutes(5)
                                           }
                                       );

            // Enable google authentication
            app.UseGoogleAuthentication(
                                           new GoogleOptions
                                           {
                                               ClientId = "309638599461-a673etlplktupuk18bo12bfljgfo4tad.apps.googleusercontent.com",
                                               ClientSecret = "YQYDFkClqu6wiSCKukaQqdfW",
                                               SignInScheme = "External",
                                               AutomaticAuthenticate = false
                                           }
                                       );

            // Setup Authorization Server
            app.UseOAuthAuthorizationServer(
                                               options => 
                                               {
                                                   options.AuthorizeEndpointPath = new PathString(Paths.AuthorizePath);
                                                   options.TokenEndpointPath = new PathString(Paths.TokenPath);
                                                   options.ApplicationCanDisplayErrors = true;
                                               
#if DEBUG                                      
                                                   options.AllowInsecureHttp = true;
#endif                                         
                                               
                                                   options.Provider = new OAuthAuthorizationServerProvider
                                                   {
                                                       OnValidateClientRedirectUri = ValidateClientRedirectUri,
                                                       OnValidateClientAuthentication = ValidateClientAuthentication,
                                                       OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
                                                       OnGrantClientCredentials = GrantClientCredetails
                                                   };
                                               
                                                   options.AuthorizationCodeProvider = new AuthenticationTokenProvider
                                                   {
                                                       OnCreate = CreateAuthenticationCode,
                                                       OnReceive = ReceiveAuthenticationCode,
                                                   };
                                               
                                                   options.RefreshTokenProvider = new AuthenticationTokenProvider
                                                   {
                                                       OnCreate = CreateRefreshToken,
                                                       OnReceive = ReceiveRefreshToken,
                                                   };
                                               
                                                   options.AutomaticAuthenticate = false;
                                               }
                                           );

            app.UseMvc(
                          routes =>
                          {
                              routes.MapRoute(
                                                 "DefaultMvc",
                                                 "{controller}/{action}/{id?}"
                                             );

                          }
                      );

            app.Run(
                       async (context) =>
                       {
                           await context.Response.WriteAsync("Katana Authorization Server");
                       }
                   );
        }
    }
}
