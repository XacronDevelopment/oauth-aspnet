using System.Web.Http;

namespace ResourceServer1.Controllers
{    
    public class MeController : ApiController
    {
        [Authorize]
        public string Get()
        {
            return this.User.Identity.Name;
        }

        [HttpGet]
        public string Test()
        {
            return "Insecure data test";
        }
    }

}
