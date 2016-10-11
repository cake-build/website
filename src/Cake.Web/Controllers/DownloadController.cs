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
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/build.ps1");
        }

        public ActionResult PowerShell()
        {
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/build.ps1");
        }

        public ActionResult Linux()
        {
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        // ReSharper disable once InconsistentNaming
        public ActionResult OSX()
        {
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        public ActionResult Bash()
        {
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/build.sh");
        }

        public ActionResult Configuration()
        {
            return Download("https://raw.githubusercontent.com/cake-build/resources/master/cake.config");
        }

        public ActionResult Packages()
        {
            var result = File(_content.Data, "text/xml", "packages.config");
            Response.Charset = "utf-8";
            return result;
        }

        private ActionResult Download(string url)
        {
            using (var client = new System.Net.WebClient())
            {
                return File(
                    client.DownloadData(url),
                    "text/plain; charset=utf-8"
                    );
            }
        }
    }
}
