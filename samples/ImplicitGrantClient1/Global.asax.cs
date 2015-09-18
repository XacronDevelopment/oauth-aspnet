using System.Web.Routing;

namespace ImplicitGrantClient1
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}