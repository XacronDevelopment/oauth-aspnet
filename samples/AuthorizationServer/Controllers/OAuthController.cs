using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Authentication;
using System.Threading.Tasks;

namespace AuthorizationServer.Controllers
{
    public class OAuthController : Controller
    {
        public async Task<ActionResult> Authorize()
        {
            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError");
            }

            AuthenticationManager authentication = Request.HttpContext.Authentication;

            ClaimsPrincipal principal = await authentication.AuthenticateAsync("Application");

            if (principal == null)
            {
                await authentication.ChallengeAsync("Application");

                if (Response.StatusCode == 200)
                    return new UnauthorizedResult();

                return new StatusCodeResult(Response.StatusCode);
            }

            string[] scopes = { };

            if (Request.QueryString.HasValue)
            {
                var queryStringComponents = QueryHelpers.ParseQuery(Request.QueryString.Value);

                if (queryStringComponents.ContainsKey("scope"))
                    scopes = queryStringComponents["scope"];
            }

            if (Request.Method == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form["submit.Grant"]))
                {
                    principal = new ClaimsPrincipal(principal.Identities);

                    ClaimsIdentity primaryIdentity = (ClaimsIdentity)principal.Identity;

                    foreach (var scope in scopes)
                    {
                        primaryIdentity.AddClaim(new Claim("urn:oauth:scope", scope));
                    }

                    await authentication.SignInAsync("Bearer", principal);
                }

                if (!string.IsNullOrEmpty(Request.Form["submit.Login"]))
                {
                    await authentication.SignOutAsync("Application");

                    await authentication.ChallengeAsync("Application");

                    return new UnauthorizedResult();
                }
            }

            return View();
        }
    }
}