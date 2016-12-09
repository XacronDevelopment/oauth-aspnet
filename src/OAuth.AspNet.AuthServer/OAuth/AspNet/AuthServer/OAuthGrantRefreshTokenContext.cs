using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Provides context information used when granting an OAuth refresh token.
    /// </summary>
    public class OAuthGrantRefreshTokenContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthGrantRefreshTokenContext"/> class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="ticket"></param>
        /// <param name="clientId"></param>
        public OAuthGrantRefreshTokenContext(HttpContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, string clientId) : base(context, options, ticket)
        {
            ClientId = clientId;
        }

        /// <summary>
        /// The OAuth client id.
        /// </summary>
        public string ClientId { get; private set; }
    }

}
