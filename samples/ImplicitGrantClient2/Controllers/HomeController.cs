using Microsoft.AspNet.Mvc;

namespace ImplicitGrantClient2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
    }
}