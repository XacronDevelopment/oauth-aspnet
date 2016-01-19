using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;
using System;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Base class for OAuth server endpoint contexts
    /// </summary>
    public class BaseOAuthEndpointContext : BaseContext
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="BaseOAuthEndpointContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> to use for this endpoint context.</param>
        /// <param name="options">The <see cref="OAuthAuthorizationServerOptions"/> to use for this endpoint context.</param>
        public BaseOAuthEndpointContext(HttpContext context, OAuthAuthorizationServerOptions options) : base(context)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            Options = options;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets the OAuth server options.
        /// </summary>
        public OAuthAuthorizationServerOptions Options { get; }

        #endregion
    }

}
