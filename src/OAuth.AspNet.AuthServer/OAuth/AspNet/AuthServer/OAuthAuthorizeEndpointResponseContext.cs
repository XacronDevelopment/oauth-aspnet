using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Provides context information when processing an Authorization Response
    /// </summary>
    public class OAuthAuthorizationEndpointResponseContext : BaseOAuthEndpointContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthAuthorizationEndpointResponseContext"/> class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <param name="ticket"></param>
        /// <param name="tokenEndpointRequest"></param>
        public OAuthAuthorizationEndpointResponseContext(HttpContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, AuthorizeEndpointRequest authorizeEndpointRequest, string accessToken, string authorizationCode) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            Principal = ticket.Principal;
            Properties = ticket.Properties;
            AuthorizeEndpointRequest = authorizeEndpointRequest;
            AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            AccessToken = accessToken;
            AuthorizationCode = authorizationCode;
        }

        /// <summary>
        /// Gets the identity of the resource owner.
        /// </summary>
        public ClaimsPrincipal Principal { get; private set; }

        /// <summary>
        /// Dictionary containing the state of the authentication session.
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }

        /// <summary>
        /// Gets information about the authorize endpoint request. 
        /// </summary>
        public AuthorizeEndpointRequest AuthorizeEndpointRequest { get; private set; }

        /// <summary>
        /// Enables additional values to be appended to the token response.
        /// </summary>
        public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

        /// <summary>
        /// The serialized Access-Token. Depending on the flow, it can be null.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// The created Authorization-Code. Depending on the flow, it can be null.
        /// </summary>
        public string AuthorizationCode { get; private set; }
    }

}
