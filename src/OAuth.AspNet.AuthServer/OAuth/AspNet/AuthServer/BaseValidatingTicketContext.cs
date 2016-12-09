using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;

namespace OAuth.AspNet.AuthServer
{

    /// <summary>
    /// Base class used for certain event contexts
    /// </summary>
    public abstract class BaseValidatingTicketContext<TOptions> : BaseValidatingContext<TOptions> where TOptions : AuthenticationOptions
    {
        /// <summary>
        /// Initializes base class used for certain event contexts
        /// </summary>
        protected BaseValidatingTicketContext(HttpContext context, TOptions options, AuthenticationTicket ticket) : base(context, options)
        {
            Ticket = ticket;
        }

        /// <summary>
        /// Contains the identity and properties for the application to authenticate. If the Validated method
        /// is invoked with an AuthenticationTicket or ClaimsIdentity argument, that new value is assigned to 
        /// this property in addition to changing IsValidated to true.
        /// </summary>
        public AuthenticationTicket Ticket { get; private set; }

        /// <summary>
        /// Replaces the ticket information on this context and marks it as as validated by the application. 
        /// IsValidated becomes true and HasError becomes false as a result of calling.
        /// </summary>
        /// <param name="ticket">Assigned to the Ticket property</param>
        /// <returns>True if the validation has taken effect.</returns>
        public bool Validated(AuthenticationTicket ticket)
        {
            Ticket = ticket;
            return Validated();
        }

        /// <summary>
        /// Alters the ticket information on this context and marks it as as validated by the application. 
        /// IsValidated becomes true and HasError becomes false as a result of calling.
        /// </summary>
        /// <param name="principal">Assigned to the Ticket.Identity property</param>
        /// <returns>True if the validation has taken effect.</returns>
        public bool Validated(ClaimsPrincipal principal)
        {
            AuthenticationProperties properties = Ticket != null ? Ticket.Properties : new AuthenticationProperties();
            return Validated(new AuthenticationTicket(principal, properties, Options.AuthenticationScheme));
        }
    }

}
