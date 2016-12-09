using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;

namespace OAuth.AspNet.AuthServer
{

    public class AuthenticationTokenCreateContext : BaseContext
    {
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        public AuthenticationTokenCreateContext(HttpContext context, ISecureDataFormat<AuthenticationTicket> secureDataFormat, AuthenticationTicket ticket) : base(context)
        {
            if (secureDataFormat == null)
                throw new ArgumentNullException(nameof(secureDataFormat));

            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            _secureDataFormat = secureDataFormat;

            Ticket = ticket;
        }

        public string Token { get; protected set; }

        public AuthenticationTicket Ticket { get; protected set; }

        public string SerializeTicket()
        {
            return _secureDataFormat.Protect(Ticket);
        }

        public void SetToken(string tokenValue)
        {
            if (tokenValue == null)
                throw new ArgumentNullException(nameof(tokenValue));

            Token = tokenValue;
        }
    }

}
