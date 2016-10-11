using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cake.Web.Core.Content.Modules;
using Cake.Web.Models;

namespace Cake.Web.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ModuleIndex _index;

        public ModuleController(ModuleIndex index)
        {
            _index = index;
        }

        public ActionResult Index(string category = null)
        {
            IReadOnlyList<Module> modules;

            if (category != null)
            {
                modules = _index.GetModulesByCategory(category);
                if (modules == null)
                {
                    throw new InvalidOperationException("No such category.");
                }
            }
            else
            {
                modules = _index.GetModules();
            }

            // Create the addin view model.
            var model = new ModuleViewModel(modules, _index.GetCategories());
            if (category != null)
            {
                model.Category = _index.GetCategoryName(category);
            }

            // Return the view.
            return View(model);
        }
    }
}