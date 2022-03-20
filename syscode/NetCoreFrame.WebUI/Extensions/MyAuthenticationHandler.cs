using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreFrame.WebUI.Extensions
{
    public class MyAuthenticationHandler : IAuthenticationHandler, IAuthenticationSignInHandler, IAuthenticationSignOutHandler
    {
        public AuthenticationScheme Scheme { get; private set; }
        protected HttpContext Context { get; private set; }


        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var cookie = Context.Request.Cookies["erpcookie"];

            if (string.IsNullOrEmpty(cookie))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            return Task.FromResult(AuthenticateResult.Success(Deserialize(cookie)));
        }

        private AuthenticationTicket Deserialize(string cookie)
        {
            throw new NotImplementedException();
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            Context.Response.Redirect("/login");
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            Context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            Scheme = scheme;
            Context = context;
            return Task.CompletedTask;
        }

        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            var ticket = new AuthenticationTicket(user, properties, Scheme.Name);
            Context.Response.Cookies.Append("erpCookie", Serialize(ticket));
            return Task.CompletedTask;
        }

        private string Serialize(AuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync(AuthenticationProperties properties)
        {
            Context.Response.Cookies.Delete("erpCookie");
            return Task.CompletedTask;
        }
    }
}
