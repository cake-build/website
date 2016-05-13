using System.Web.Mvc;
using Cake.Web.Core.Content;

namespace Cake.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly PackagesConfigContent _content;

        public DownloadController(PackagesConfigContent content)
        {
            _content = content;
        }

        public ActionResult Windows()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/build.ps1");
        }

        public ActionResult PowerShell()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/build.ps1");
        }

        public ActionResult Linux()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        // ReSharper disable once InconsistentNaming
        public ActionResult OSX()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        public ActionResult Bash()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        public ActionResult Configuration()
        {
            return Redirect("https://raw.githubusercontent.com/cake-build/resources/master/cake.config");
        }

        public ActionResult Packages()
        {
            var result = File(_content.Data, "text/xml", "packages.config");
            Response.Charset = "utf-8";
            return result;
        }
    }
}
