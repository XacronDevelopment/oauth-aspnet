using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.Controllers
{
    public class AccountController : Controller
    {
        public async Task<ActionResult> Login()
        {
            AuthenticationManager authentication = Request.HttpContext.Authentication;

            if (Request.Method == "POST")
            {
                var isPersistent = !string.IsNullOrEmpty(Request.Form["isPersistent"]);

                if (!string.IsNullOrEmpty(Request.Form["submit.Signin"]))
                {
                    var authenticationPrincipal = new ClaimsPrincipal(new[] { new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["username"]) }, "Application") });

                    await authentication.SignInAsync("Application", authenticationPrincipal, new AuthenticationProperties { IsPersistent = isPersistent });
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public async Task<ActionResult> External()
        {
            var authentication = Request.HttpContext.Authentication;
            if (Request.Method == "POST")
            {
                foreach (var key in Request.Form.Keys)
                {
                    if (key.StartsWith("submit.External.") && !string.IsNullOrEmpty(Request.Form[key]))
                    {
                        var authType = key.Substring("submit.External.".Length);

                        await authentication.ChallengeAsync(authType);

                        if (Response.StatusCode == 200)
                            return new HttpUnauthorizedResult();

                        return new HttpStatusCodeResult(Response.StatusCode);
                    }
                }
            }

            ClaimsPrincipal principal = await authentication.AuthenticateAsync("External");
            if (principal != null)
            {                
                await authentication.SignOutAsync("External");

                await authentication.SignInAsync(
                                                    "Application",
                                                    new ClaimsPrincipal(principal.Identities),
                                                    new AuthenticationProperties { IsPersistent = true }                                         
                                                );

                string[] returnUrls = { };

                if (Request.QueryString.HasValue)
                {
                    var queryStringComponents = QueryHelpers.ParseQuery(Request.QueryString.Value);

                    if (queryStringComponents.ContainsKey("ReturnUrl"))
                        returnUrls = queryStringComponents["ReturnUrl"];
                }                

                return Redirect(returnUrls[0]);
            }

            return View();
        }
    }
}