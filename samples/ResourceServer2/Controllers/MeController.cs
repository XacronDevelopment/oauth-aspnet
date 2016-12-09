using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServer2
{

    [Authorize]
    public class MeController : Controller
    {
        public string Get()
        {
            return User.Identity.Name;
        }
    }

}
