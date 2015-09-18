using Microsoft.AspNet.Http;
using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Provides context information used in handling an OAuth client credentials grant.
    /// </summary>
    public class OAuthGrantClientCredentialsContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthGrantClientCredentialsContext"/> class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="clientId"></param>
        /// <param name="scope"></param>
        public OAuthGrantClientCredentialsContext(HttpContext context, OAuthAuthorizationServerOptions options, string clientId, IList<string> scope) : base(context, options, null)
        {
            ClientId = clientId;
            Scope = scope;
        }

        /// <summary>
        /// OAuth client id.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// List of scopes allowed by the resource owner.
        /// </summary>
        public IList<string> Scope { get; private set; }
    }

}
