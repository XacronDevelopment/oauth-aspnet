using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Data object used by TokenEndpointRequest which contains parameter information when the "grant_type" is unrecognized.
    /// </summary>
    public class TokenEndpointRequestCustomExtension
    {
        /// <summary>
        /// The parameter information when the "grant_type" is unrecognized.
        /// </summary>
        public IDictionary<string, StringValues> Parameters { get; set; }
    }

}
