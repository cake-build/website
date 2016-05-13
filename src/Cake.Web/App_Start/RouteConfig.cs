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

            RegisterDownloads(routes);

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static void RegisterDownloads(RouteCollection routes)
        {
            // Downloads: Windows bootstrapper
            routes.MapRoute(
                "DownloadBootstrapperWindows", "download/bootstrapper/windows",
                new { controller = "Download", action = "Windows" }
                );

            // Downloads: PowerShell bootstrapper
            routes.MapRoute(
                "DownloadBootstrapperPowerShell", "download/bootstrapper/powershell",
                new { controller = "Download", action = "PowerShell" }
                );

            // Downloads: OSX bootstrapper
            routes.MapRoute(
                "DownloadBootstrapperOSX", "download/bootstrapper/osx",
                new { controller = "Download", action = "OSX" }
                );

            // Downloads: Linux bootstrapper
            routes.MapRoute(
                "DownloadBootstrapperLinux", "download/bootstrapper/linux",
                new { controller = "Download", action = "Linux" }
                );

            // Downloads: Bash bootstrapper
            routes.MapRoute(
                "DownloadBootstrapperBash", "download/bootstrapper/bash",
                new { controller = "Download", action = "Bash" }
                );

            // Downloads: Bootstrapper packages
            routes.MapRoute(
                "DownloadBootstrapperPackages", "download/bootstrapper/packages",
                new { controller = "Download", action = "Packages" }
                );

            // Downloads: Configuration
            routes.MapRoute(
                "DownloadConfiguration", "download/configuration",
                new { controller = "Download", action = "Configuration" }
                );
        }
    }
}
