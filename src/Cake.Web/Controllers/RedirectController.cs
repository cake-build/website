using System.Web.Mvc;

namespace Cake.Web.Controllers
{
    public class RedirectController : Controller
    {
        public ActionResult Index(string path)
        {
            return RedirectPermanent(path);
        }
    }
}