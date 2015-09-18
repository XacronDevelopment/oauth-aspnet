using Microsoft.AspNet.Http;
using Microsoft.AspNet.Authentication;
using System;

namespace OAuth.AspNet.AuthServer
{

    public class AuthenticationTokenReceiveContext : BaseContext
    {
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        public AuthenticationTokenReceiveContext(HttpContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, string token) : base(context)
        {
            if (secureDataFormat == null)
                throw new ArgumentNullException(nameof(secureDataFormat));

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            _secureDataFormat = secureDataFormat;

            Token = token;
        }

        public string Token { get; protected set; }

        public AuthenticationTicket Ticket { get; protected set; }

        public void DeserializeTicket(string protectedData)
        {
            Ticket = _secureDataFormat.Unprotect(protectedData);
        }

        public void SetTicket(AuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            Ticket = ticket;
        }
    }

}
