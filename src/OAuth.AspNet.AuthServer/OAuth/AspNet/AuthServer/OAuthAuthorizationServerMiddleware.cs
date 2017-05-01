using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Authorization Server middleware component which is added to an OWIN pipeline. This class is not
    /// created by application code directly, instead it is added by calling the the IAppBuilder UseOAuthAuthorizationServer 
    /// extension method.
    /// </summary>
    public class OAuthAuthorizationServerMiddleware : AuthenticationMiddleware<OAuthAuthorizationServerOptions>
    {
        /// <summary>
        /// Authorization Server middleware component which is added to an OWIN pipeline. This constructor is not
        /// called by application code directly, instead it is added by calling the the IAppBuilder UseOAuthAuthorizationServer 
        /// extension method.
        /// </summary>
        public OAuthAuthorizationServerMiddleware(RequestDelegate next, IOptions<OAuthAuthorizationServerOptions> options, ILoggerFactory loggerFactory, IDataProtectionProvider dataProtectionProvider, UrlEncoder encoder) : base(next, options, loggerFactory, encoder)
        {
            if (Options.Provider == null)
            {
                Options.Provider = new OAuthAuthorizationServerProvider();
            }

            if (Options.AuthorizationCodeFormat == null)
            {
                IDataProtector dataProtecter = dataProtectionProvider.CreateProtector(typeof(OAuthAuthorizationServerMiddleware).FullName, "Authentication_Code", "v1");

                Options.AuthorizationCodeFormat = new TicketDataFormat(dataProtecter);
            }

            if (Options.RefreshTokenFormat == null)
            {
                IDataProtector dataProtecter = dataProtectionProvider.CreateProtector(typeof(OAuthAuthorizationServerMiddleware).FullName, "Refresh_Token", "v1");

                Options.RefreshTokenFormat = new TicketDataFormat(dataProtecter);
            }

            if (Options.TokenDataProtector == null)
            {
                Options.TokenDataProtector = DataProtectionProvider.Create(new DirectoryInfo(Environment.GetEnvironmentVariable("Temp"))).CreateProtector("OAuth.AspNet.AuthServer");
            }

            if (Options.AccessTokenFormat == null)
            {
                IDataProtector dataProtecter = Options.TokenDataProtector.CreateProtector("Access_Token", "v1");

                Options.AccessTokenFormat = new TicketDataFormat(dataProtecter);
            }

            if (Options.AuthorizationCodeProvider == null)
            {
                Options.AuthorizationCodeProvider = new AuthenticationTokenProvider();
            }

            if (Options.AccessTokenProvider == null)
            {
                Options.AccessTokenProvider = new AuthenticationTokenProvider();
            }

            if (Options.RefreshTokenProvider == null)
            {
                Options.RefreshTokenProvider = new AuthenticationTokenProvider();
            }
        }

        /// <summary>
        /// Called by the AuthenticationMiddleware base class to create a per-request handler. 
        /// </summary>
        /// <returns>A new instance of the request handler</returns>
        protected override AuthenticationHandler<OAuthAuthorizationServerOptions> CreateHandler()
        {
            return new OAuthAuthorizationServerHandler();
        }
    }

}