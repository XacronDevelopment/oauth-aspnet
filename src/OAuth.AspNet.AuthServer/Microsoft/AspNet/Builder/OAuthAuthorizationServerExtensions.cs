using System;
using OAuth.AspNet.AuthServer;

namespace Microsoft.AspNetCore.Builder
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
        public static IApplicationBuilder UseOAuthAuthorizationServer(this IApplicationBuilder app, OAuthAuthorizationServerOptions options)
        {
            if (app == null)
                throw new NullReferenceException($"The extension method {nameof(OAuthAuthorizationServerExtensions.UseOAuthAuthorizationServer)} was called on a null reference to a {nameof(IApplicationBuilder)}");

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<OAuthAuthorizationServerMiddleware>(options);
        }


        /// <summary>
        /// Adds OAuth2 Authorization Server capabilities to an OWIN web application. This middleware
        /// performs the request processing for the Authorize and Token endpoints defined by the OAuth2 specification.
        /// See also http://tools.ietf.org/html/rfc6749
        /// </summary>
        /// <param name="app">The web application builder</param>
        /// <param name="configureOptions">Options which control the behavior of the Authorization Server.</param>
        /// <returns>The application builder</returns>
        public static IApplicationBuilder UseOAuthAuthorizationServer(this IApplicationBuilder app, Action<OAuthAuthorizationServerOptions> configureOptions)
        {
            if (app == null)
                throw new NullReferenceException($"The extension method {nameof(OAuthAuthorizationServerExtensions.UseOAuthAuthorizationServer)} was called on a null reference to a {nameof(IApplicationBuilder)}");

            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));


            var options = new OAuthAuthorizationServerOptions();
            if (configureOptions != null)

                configureOptions(options);

            return app.UseOAuthAuthorizationServer(options);
        }
    }

}
