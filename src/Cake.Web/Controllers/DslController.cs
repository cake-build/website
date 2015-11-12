using System.Web.Mvc;
using Cake.Web.Core.Dsl;

namespace Cake.Web.Controllers
{
    public class DslController : Controller
    {
        private readonly DslModel _model;

        public DslController(DslModel model)
        {
            _model = model;
        }

        public ActionResult Index(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return View(_model);
            }

            // Try to find the category.
            var category = _model.FindCategory(path);
            if (category != null)
            {
                return View("Category", category);
            }

            // The category could not be found.
            var message = $"Could not find DSL category '{path}'.";
            return HttpNotFound(message);
        }
    }
}