using Microsoft.Owin.Security.OAuth;
using OAuth.Owin.Tokens;
using Owin;

namespace ResourceServer1
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var options = new OAuthBearerAuthenticationOptions()
                          {
                              AccessTokenFormat = new TicketDataFormat()
                          };

            app.UseOAuthBearerAuthentication(options);
        }
    }
}