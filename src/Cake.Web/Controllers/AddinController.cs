using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cake.Web.Core.Content.Addins;
using Cake.Web.Models;

namespace Cake.Web.Controllers
{
    public class AddinController : Controller
    {
        private readonly AddinIndex _index;

        public AddinController(AddinIndex index)
        {
            _index = index;
        }

        public ActionResult Index(string category = null)
        {
            IReadOnlyList<Addin> addins = null;

            if (category != null)
            {
                addins = _index.GetAddinsByCategory(category);
                if (addins == null)
                {
                    throw new InvalidOperationException("No such category.");
                }
            }
            else
            {
                addins = _index.GetAddins();
            }

            // Create the addin view model.
            var model = new AddinViewModel(addins, _index.GetCategories());
            if (category != null)
            {
                model.Category = _index.GetCategoryName(category);
            }

            // Return the view.
            return View(model);
        }
    }
}