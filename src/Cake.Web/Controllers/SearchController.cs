using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cake.Web.Core.Services;

namespace Cake.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly SearchService _service;

        public SearchController(SearchService service)
        {
            _service = service;
        }

        // GET: Search
        public ActionResult Index(string query)
        {
            return new JsonResult
            {
                Data = _service.Find(query),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}