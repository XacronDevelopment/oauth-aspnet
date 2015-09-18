using System.Web.Http;

namespace ResourceServer1.Controllers
{
    [Authorize]
    public class MeController : ApiController
    {
        public string Get()
        {
            return this.User.Identity.Name;
        }
    }

}
