using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{
    /// <summary>
    /// Data object used by TokenEndpointRequest when the "grant_type" parameter is "refresh_token".
    /// </summary>
    public class TokenEndpointRequestRefreshToken
    {
        /// <summary>
        /// The value passed to the Token endpoint in the "refresh_token" parameter
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The value passed to the Token endpoint in the "scope" parameter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is just a data container object.")]
        public IList<string> Scope { get; set; }
    }
}
