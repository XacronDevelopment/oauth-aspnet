using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Provides context information when handling an OAuth authorization code grant.
    /// </summary>
    public class OAuthGrantAuthorizationCodeContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthGrantAuthorizationCodeContext"/> class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="ticket"></param>
        public OAuthGrantAuthorizationCodeContext(HttpContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket) : base(context, options, ticket) { }
    }

}
