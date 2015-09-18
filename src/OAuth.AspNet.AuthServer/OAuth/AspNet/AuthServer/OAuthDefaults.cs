
namespace OAuth.AspNet.AuthServer
{
    /// <summary>
    /// Default values used by authorization server and bearer authentication.
    /// </summary>
    public static class OAuthDefaults
    {
        /// <summary>
        /// Default value for AuthenticationType property in the OAuthBearerAuthenticationOptions and
        /// OAuthAuthorizationServerOptions.
        /// </summary>
        public const string AuthenticationType = "Bearer";
    }
}
