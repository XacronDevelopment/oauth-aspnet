using System;
using System.Threading.Tasks;

namespace OAuth.AspNet.AuthServer
{

    internal static class DefaultBehavior
    {
        internal static readonly Func<OAuthValidateAuthorizeRequestContext, Task> ValidateAuthorizeRequest = context =>
        {
            context.Validated();
            return Task.FromResult<object>(null);
        };

        internal static readonly Func<OAuthValidateTokenRequestContext, Task> ValidateTokenRequest = context =>
        {
            context.Validated();
            return Task.FromResult<object>(null);
        };

        internal static readonly Func<OAuthGrantAuthorizationCodeContext, Task> GrantAuthorizationCode = context =>
        {
            if (context.Ticket != null && context.Ticket.Principal != null && context.Ticket.Principal.Identity.IsAuthenticated)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        };

        internal static readonly Func<OAuthGrantRefreshTokenContext, Task> GrantRefreshToken = context =>
        {
            if (context.Ticket != null && context.Ticket.Principal != null && context.Ticket.Principal.Identity.IsAuthenticated)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        };
    }

}
