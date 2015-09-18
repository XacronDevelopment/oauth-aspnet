using Microsoft.AspNet.Http;

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
        public IReadableStringCollection Parameters { get; set; }
    }

}
