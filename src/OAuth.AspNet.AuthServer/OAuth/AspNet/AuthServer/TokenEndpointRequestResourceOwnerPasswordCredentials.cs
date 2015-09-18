using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{
    /// <summary>
    /// Data object used by TokenEndpointRequest when the "grant_type" is "password".
    /// </summary>    
    public class TokenEndpointRequestResourceOwnerPasswordCredentials
    {
        /// <summary>
        /// The value passed to the Token endpoint in the "username" parameter
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The value passed to the Token endpoint in the "password" parameter
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The value passed to the Token endpoint in the "scope" parameter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is just a data class.")]
        public IList<string> Scope { get; set; }
    }
}
