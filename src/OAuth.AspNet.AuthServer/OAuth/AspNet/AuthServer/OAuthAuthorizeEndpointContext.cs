using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// An event raised after the Authorization Server has processed the request, but before it is passed on to the web application.
    /// Calling RequestCompleted will prevent the request from passing on to the web application.
    /// </summary>
    public class OAuthAuthorizeEndpointContext : BaseOAuthEndpointContext
    {
        /// <summary>
        /// Creates an instance of this context
        /// </summary>
        public OAuthAuthorizeEndpointContext(HttpContext context, OAuthAuthorizationServerOptions options, AuthorizeEndpointRequest authorizeRequest) : base(context, options)
        {
            AuthorizeRequest = authorizeRequest;
        }

        /// <summary>
        /// Gets OAuth authorization request data.
        /// </summary>
        public AuthorizeEndpointRequest AuthorizeRequest { get; private set; }

        public bool IsRequestCompleted { get; private set; }

        public void RequestCompleted()
        {
            IsRequestCompleted = true;
        }
    }

}
