using Microsoft.AspNet.Http;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Base class used for certain event contexts
    /// </summary>
    public abstract class BaseValidatingClientContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Initializes base class used for certain event contexts
        /// </summary>
        protected BaseValidatingClientContext(HttpContext context, OAuthAuthorizationServerOptions options, string clientId) : base(context, options)
        {
            ClientId = clientId;
        }

        /// <summary>
        /// The "client_id" parameter for the current request. The Authorization Server application is responsible for 
        /// validating this value identifies a registered client.
        /// </summary>
        public string ClientId { get; protected set; }
    }

}
