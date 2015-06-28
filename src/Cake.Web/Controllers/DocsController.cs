using System;
using System.Net;
using System.Web.Mvc;
using Cake.Web.Core.Documentation;

namespace Cake.Web.Controllers
{
    public class DocsController : Controller
    {
        private readonly TopicTree _tree;

        public DocsController(TopicTree tree)
        {
            _tree = tree;
        }

        public ActionResult Index(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "introduction/about";
            }

            var parts = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 1)
            {
                // Is there a section with this path?
                var section = _tree.FindSection(parts[0]);
                if (section != null)
                {
                    return View("Section", section);
                }
            }
            if (parts.Length == 2)
            {
                // Is there a topic with this path?
                var topic = _tree.FindTopic(parts[0], parts[1]);
                if (topic != null)
                {
                    return View("Topic", topic);
                }
            }

            // Not found!
            // TODO: Try to resolve the URL.
            return new HttpStatusCodeResult(
                HttpStatusCode.NotFound,
                "Could not find documentation.");
        }
    }
}