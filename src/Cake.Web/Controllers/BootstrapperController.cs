using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cake.Web.Controllers
{
    public class BootstrapperController : Controller
    {
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
    }
}
