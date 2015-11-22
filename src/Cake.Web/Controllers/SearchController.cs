using System.Linq;
using System.Web.Mvc;
using Cake.Web.Core;
using Cake.Web.Core.Search;
using Cake.Web.Core.Services;
using Cake.Web.Models;

namespace Cake.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly SearchService _service;

        public SearchController(SearchService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(new SearchResultViewModel(string.Empty, null));
        }

        [HttpPost]
        [Throttle(Seconds = 1)]
        public ActionResult Index(SearchViewModel model)
        {
            if (model == null)
            {
                return View("Index", new SearchResultViewModel(model.Term, Enumerable.Empty<SearchTerm>()));
            }
            var result = _service.Find(model.Term, 10).ToList();
            return View("Index", new SearchResultViewModel(model.Term, result));
        }
    }
}