﻿using System.Web.Mvc;
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

            // Blog: Index
            routes.MapRoute(
                "Blog", "blog/",
                new { controller = "Blog", action = "index" }
            );

            // Blog: Category
            routes.MapRoute(
                "BlogCategory", "blog/category/{category}",
                new { controller = "Blog", action = "index" }
            );

            // Blog: Archive
            routes.MapRoute(
                "BlogArchive", "blog/archive/{year}/{month}",
                new { controller = "Blog", action = "index" }
            );

            // Blog: Archive
            routes.MapRoute(
                "BlogArchiveYear", "blog/archive/{year}",
                new { controller = "Blog", action = "index" }
            );

            // Blog: Post
            routes.MapRoute(
                "BlogPost", "blog/{year}/{month}/{slug}",
                new { controller = "Blog", action = "Details" }
            );

            // Blog: Feed
            routes.MapRoute(
                "BlogFeed", "blog/feed/{format}",
                new { controller = "Blog", action = "Feed" }
            );

            // Addins
            routes.MapRoute(
                "Addins", "addins/",
                new { controller = "Addin", action = "index" }
            );

            // Addins: Category
            routes.MapRoute(
                "AddinsCategory", "addins/category/{category}",
                new { controller = "Addin", action = "index" }
            );

            // Search
            routes.MapRoute(
                "Search", "search/",
                new { controller = "Search", action = "index" }
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
