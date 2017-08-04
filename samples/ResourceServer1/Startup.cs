using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(ResourceServer1.Startup))]

namespace ResourceServer1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            app.Run(context =>
            {
                return context.Response.WriteAsync("Resource Server 1 (ASP.NET 4)");
            });
        }
    }
}
