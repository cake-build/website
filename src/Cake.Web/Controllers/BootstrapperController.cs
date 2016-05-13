using System.Web.Mvc;

namespace Cake.Web.Controllers
{
    public class BootstrapperController : Controller
    {
        public ActionResult Windows()
        {
            return RedirectToActionPermanent("Windows", "Download");
        }

        public ActionResult Linux()
        {
            return RedirectToActionPermanent("Linux", "Download");
        }

        // ReSharper disable once InconsistentNaming
        public ActionResult OSX()
        {
            return RedirectToActionPermanent("OSX", "Download");
        }

        public ActionResult Packages()
        {
            return RedirectToActionPermanent("Packages", "Download");
        }
    }
}
