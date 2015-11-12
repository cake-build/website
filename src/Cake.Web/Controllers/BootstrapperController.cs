using System.Web.Mvc;
using Cake.Web.Core.Content;

namespace Cake.Web.Controllers
{
    public class BootstrapperController : Controller
    {
        private readonly PackagesConfigContent _content;

        public BootstrapperController(PackagesConfigContent content)
        {
            _content = content;
        }

        public ActionResult Windows()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/bootstrapper/master/res/scripts/build.ps1");
        }

        public ActionResult Linux()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/bootstrapper/master/res/scripts/build.sh");
        }

        // ReSharper disable once InconsistentNaming
        public ActionResult OSX()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/bootstrapper/master/res/scripts/build.sh");
        }

        public ActionResult Packages()
        {
            var result = File(_content.Data, "text/xml", "packages.config");
            Response.Charset = "utf-8";
            return result;
        }
    }
}
