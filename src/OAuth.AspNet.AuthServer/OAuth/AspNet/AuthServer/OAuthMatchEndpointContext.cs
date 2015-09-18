using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Provides notification used for determining the OAuth flow type based on the request.
    /// </summary>
    public class OAuthMatchEndpointNotification : BaseNotification<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthMatchEndpointNotification"/> class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        public OAuthMatchEndpointNotification(HttpContext context, OAuthAuthorizationServerOptions options) : base(context, options) { }

        /// <summary>
        /// Gets whether or not the endpoint is an OAuth authorize endpoint.
        /// </summary>
        public bool IsAuthorizeEndpoint { get; private set; }

        /// <summary>
        /// Gets whether or not the endpoint is an OAuth token endpoint.
        /// </summary>
        public bool IsTokenEndpoint { get; private set; }

        /// <summary>
        /// Sets the endpoint type to authorize endpoint.
        /// </summary>
        public void MatchesAuthorizeEndpoint()
        {
            IsAuthorizeEndpoint = true;
            IsTokenEndpoint = false;
        }

        /// <summary>
        /// Sets the endpoint type to token endpoint.
        /// </summary>
        public void MatchesTokenEndpoint()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = true;
        }

        /// <summary>
        /// Sets the endpoint type to neither authorize nor token.
        /// </summary>
        public void MatchesNothing()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = false;
        }
    }

}
