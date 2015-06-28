using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cake.Web
{
    public class LegacyRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Kind of hackish but will do for now.
            // If this list grows, we might have to look at another solution.
            var lookup = new Dictionary<string, string>()
            {
                {"what-is-cake", "/docs/overview/features"},
                {"getting-started", "/docs/tutorials/getting-started"},
                {"documentation", "/docs"},
                {"documentation/tasks", "/docs/fundamentals/tasks"},
                {"documentation/dependencies", "/docs/fundamentals/dependencies"},
                {"documentation/criterias", "/docs/fundamentals/criterias"},
                {"documentation/error-handling", "/docs/fundamentals/error-handling"},
                {"documentation/error-reporting", "/docs/fundamentals/error-handling"},
                {"documentation/finally-block", "/docs/fundamentals/finally-block"},
                {"documentation/setup-and-teardown", "/docs/fundamentals/setup-and-teardown"},
                {"documentation/running-targets", "/docs/fundamentals/running-targets"},
                {"documentation/script-aliases", "/docs/fundamentals/aliases"},
                {"contribute", "/docs/contributing/guidelines"},
                {"contribute/contribution-guidelines", "/docs/contributing/guidelines"},
            };

            var index = 1;
            foreach (var pair in lookup)
            {
                routes.MapRoute(
                    string.Format("Legacy{0}", index),
                    pair.Key,
                    new { controller = "Redirect", action = "Index", path = pair.Value});

                index++;
            }
        }
    }
}