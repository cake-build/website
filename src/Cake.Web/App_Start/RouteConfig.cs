using System.Web.Mvc;
using System.Web.Routing;

namespace Cake.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Legacy routes
            LegacyRouteConfig.RegisterRoutes(routes);

            // Documentation
            routes.MapRoute(
                "Documentation", "docs/{*path}",
                new { controller = "Docs", action = "index" }
            );

            // DSL Reference
            routes.MapRoute(
                "DSL", "dsl/{*path}",
                new { controller = "Dsl", action = "index" }
            );

            // API Reference
            routes.MapRoute(
                "API", "api/",
                new { controller = "Api", action = "index" }
            );

            // API Reference: Namespace
            routes.MapRoute(
                "Namespace", "api/{namespaceId}",
                new { controller = "Api", action = "namespace" }
            );

            // API Reference: Type
            routes.MapRoute(
                "Type", "api/{namespaceId}/{typeId}",
                new { controller = "Api", action = "type" }
            );

            // API Reference: Method
            routes.MapRoute(
                "Member", "api/{namespaceId}/{typeId}/{memberId}",
                new { controller = "Api", action = "member" }
            );

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
