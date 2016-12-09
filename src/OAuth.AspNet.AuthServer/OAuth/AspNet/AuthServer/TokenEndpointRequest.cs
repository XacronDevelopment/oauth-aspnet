using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Data object representing the information contained in form encoded body of a Token endpoint request.
    /// </summary>
    public class TokenEndpointRequest
    {
        /// <summary>
        /// Creates a new instance populated with values from the form encoded body parameters.
        /// </summary>
        /// <param name="parameters">Form encoded body parameters from a request.</param>
        public TokenEndpointRequest(IDictionary<string, StringValues> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            Parameters = parameters;

            StringValues grantTypeValue;
            parameters.TryGetValue(Constants.Parameters.GrantType, out grantTypeValue);
            GrantType = grantTypeValue;

            StringValues clientIdValue;
            parameters.TryGetValue(Constants.Parameters.ClientId, out clientIdValue);
            ClientId = clientIdValue;

            if (string.Equals(GrantType, Constants.GrantTypes.AuthorizationCode, StringComparison.Ordinal))
            {
                StringValues codeValue;
                parameters.TryGetValue(Constants.Parameters.Code, out codeValue);

                StringValues redirectUrlValue;
                parameters.TryGetValue(Constants.Parameters.RedirectUri, out redirectUrlValue);

                AuthorizationCodeGrant = new TokenEndpointRequestAuthorizationCode
                {
                    Code = codeValue,
                    RedirectUri = redirectUrlValue
                };
            }
            else if (string.Equals(GrantType, Constants.GrantTypes.ClientCredentials, StringComparison.Ordinal))
            {
                StringValues scopeValue;
                parameters.TryGetValue(Constants.Parameters.Scope, out scopeValue);

                ClientCredentialsGrant = new TokenEndpointRequestClientCredentials
                {
                    Scope = ((string)scopeValue ?? string.Empty).Split(' ')
                };
            }
            else if (string.Equals(GrantType, Constants.GrantTypes.RefreshToken, StringComparison.Ordinal))
            {
                StringValues refreshTokenValue;
                parameters.TryGetValue(Constants.Parameters.RefreshToken, out refreshTokenValue);

                StringValues scopeValue;
                parameters.TryGetValue(Constants.Parameters.Scope, out scopeValue);

                RefreshTokenGrant = new TokenEndpointRequestRefreshToken
                {
                    RefreshToken = refreshTokenValue,
                    Scope = ((string)scopeValue ?? string.Empty).Split(' ')
                };
            }
            else if (string.Equals(GrantType, Constants.GrantTypes.Password, StringComparison.Ordinal))
            {
                StringValues usernameValue;
                parameters.TryGetValue(Constants.Parameters.Username, out usernameValue);

                StringValues passwordValue;
                parameters.TryGetValue(Constants.Parameters.Scope, out passwordValue);

                StringValues scopeValue;
                parameters.TryGetValue(Constants.Parameters.Scope, out scopeValue);

                ResourceOwnerPasswordCredentialsGrant = new TokenEndpointRequestResourceOwnerPasswordCredentials
                {
                    UserName = usernameValue,
                    Password = passwordValue,
                    Scope = ((string)scopeValue ?? string.Empty).Split(' ')
                };
            }
            else if (!string.IsNullOrEmpty(GrantType))
            {
                CustomExtensionGrant = new TokenEndpointRequestCustomExtension
                {
                    Parameters = parameters,
                };
            }
        }

        /// <summary>
        /// The form encoded body parameters of the Token endpoint request
        /// </summary>
        public IDictionary<string, StringValues> Parameters { get; private set; }

        /// <summary>
        /// The "grant_type" parameter of the Token endpoint request. This parameter is required.
        /// </summary>
        public string GrantType { get; private set; }

        /// <summary>
        /// The "client_id" parameter of the Token endpoint request. This parameter is optional. It might not
        /// be present if the request is authenticated in a different way, for example, by using basic authentication
        /// credentials.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Data object available when the "grant_type" is "authorization_code".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.1.3
        /// </summary>    
        public TokenEndpointRequestAuthorizationCode AuthorizationCodeGrant { get; private set; }

        /// <summary>
        /// Data object available when the "grant_type" is "client_credentials".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.4.2
        /// </summary>    
        public TokenEndpointRequestClientCredentials ClientCredentialsGrant { get; private set; }

        /// <summary>
        /// Data object available when the "grant_type" is "refresh_token".
        /// See also http://tools.ietf.org/html/rfc6749#section-6
        /// </summary>    
        public TokenEndpointRequestRefreshToken RefreshTokenGrant { get; private set; }

        /// <summary>
        /// Data object available when the "grant_type" is "password".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>    
        public TokenEndpointRequestResourceOwnerPasswordCredentials ResourceOwnerPasswordCredentialsGrant { get; private set; }

        /// <summary>
        /// Data object available when the "grant_type" is unrecognized.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.5
        /// </summary>
        public TokenEndpointRequestCustomExtension CustomExtensionGrant { get; private set; }

        /// <summary>
        /// True when the "grant_type" is "authorization_code".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.1.3
        /// </summary>    
        public bool IsAuthorizationCodeGrantType
        {
            get { return AuthorizationCodeGrant != null; }
        }

        /// <summary>
        /// True when the "grant_type" is "client_credentials".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.4.2
        /// </summary>  
        public bool IsClientCredentialsGrantType
        {
            get { return ClientCredentialsGrant != null; }
        }

        /// <summary>
        /// True when the "grant_type" is "refresh_token".
        /// See also http://tools.ietf.org/html/rfc6749#section-6
        /// </summary>    
        public bool IsRefreshTokenGrantType
        {
            get { return RefreshTokenGrant != null; }
        }

        /// <summary>
        /// True when the "grant_type" is "password".
        /// See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>    
        public bool IsResourceOwnerPasswordCredentialsGrantType
        {
            get { return ResourceOwnerPasswordCredentialsGrant != null; }
        }

        /// <summary>
        /// True when the "grant_type" is unrecognized.
        /// See also http://tools.ietf.org/html/rfc6749#section-4.5
        /// </summary>
        public bool IsCustomExtensionGrantType
        {
            get { return CustomExtensionGrant != null; }
        }
    }

}
