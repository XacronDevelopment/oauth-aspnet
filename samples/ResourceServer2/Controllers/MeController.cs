using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

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
