using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Data object used by TokenEndpointRequest when the "grant_type" is "client_credentials".
    /// </summary>    
    public class TokenEndpointRequestClientCredentials
    {
        /// <summary>
        /// The value passed to the Token endpoint in the "scope" parameter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This class is just for passing data through.")]
        public IList<string> Scope { get; set; }
    }

}
