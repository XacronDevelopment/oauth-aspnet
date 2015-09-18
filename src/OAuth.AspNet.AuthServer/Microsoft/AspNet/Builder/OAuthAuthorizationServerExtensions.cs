using System;
using Microsoft.Framework.OptionsModel;
using OAuth.AspNet.AuthServer;

namespace Microsoft.AspNet.Builder
{

    /// <summary>
    /// Extension methods to add Authorization Server capabilities to an OWIN pipeline
    /// </summary>
    public static class OAuthAuthorizationServerExtensions
    {

        /// <summary>
        /// Adds OAuth2 Authorization Server capabilities to an OWIN web application. This middleware
        /// performs the request processing for the Authorize and Token endpoints defined by the OAuth2 specification.
        /// See also http://tools.ietf.org/html/rfc6749
        /// </summary>
        /// <param name="app">The web application builder</param>
        /// <param name="options">Options which control the behavior of the Authorization Server.</param>
        /// <returns>The application builder</returns>
        public static IApplicationBuilder UseOAuthAuthorizationServer(this IApplicationBuilder app, Action<OAuthAuthorizationServerOptions> configureOptions)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));


            return app.UseMiddleware<OAuthAuthorizationServerMiddleware>(new ConfigureOptions<OAuthAuthorizationServerOptions>(configureOptions ?? (options => { })));
        }
    }

}
