using System;
using System.Threading.Tasks;

namespace OAuth.AspNet.AuthServer
{

    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        public Action<AuthenticationTokenCreateContext> OnCreate { get; set; }
        public Func<AuthenticationTokenCreateContext, Task> OnCreateAsync { get; set; }
        public Action<AuthenticationTokenReceiveContext> OnReceive { get; set; }
        public Func<AuthenticationTokenReceiveContext, Task> OnReceiveAsync { get; set; }

        public virtual void Create(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException("Authentication token did not provide an OnCreate method.");
            }
            if (OnCreate != null)
            {
                OnCreate.Invoke(context);
            }
        }

        public virtual async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            if (OnCreateAsync != null && OnCreate == null)
            {
                throw new InvalidOperationException("Authentication token did not provide an OnCreate method.");
            }
            if (OnCreateAsync != null)
            {
                await OnCreateAsync.Invoke(context);
            }
            else
            {
                Create(context);
            }
        }

        public virtual void Receive(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException("Authentication token did not provide an OnReceive method.");
            }

            if (OnReceive != null)
            {
                OnReceive.Invoke(context);
            }
        }

        public virtual async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (OnReceiveAsync != null && OnReceive == null)
            {
                throw new InvalidOperationException("Authentication token did not provide an OnReceive method.");
            }
            if (OnReceiveAsync != null)
            {
                await OnReceiveAsync.Invoke(context);
            }
            else
            {
                Receive(context);
            }
        }
    }

}
