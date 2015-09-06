using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Cake.Core.IO;
using Cake.Web.Core;
using Cake.Web.Core.Content;
using Cake.Web.Core.Content.Addins;
using Cake.Web.Core.Content.Blog;
using Cake.Web.Core.Content.Documentation;
using Cake.Web.Core.Dsl;
using Cake.Web.Core.NuGet;
using Cake.Web.Core.Rendering;
using Cake.Web.Core.Services;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Get the application data path.
            var appDataPath = new DirectoryPath(AppDomain.CurrentDomain.GetData("DataDirectory").ToString());

            // Create the document model by downloading the nuget package.
            var cakeVersion = (string)null;
            var documentModel = NuGetBootstrapper.Download(appDataPath,
                new NuGetConfiguration {
                    Packages = new List<PackageDefinition>
                {
                    new PackageDefinition
                    {
                        Filters = new List<string>
                        {
                            "/**/Cake.Core.dll",
                            "/**/Cake.Common.dll",
                            "/**/Cake.Core.xml",
                            "/**/Cake.Common.xml"
                        },
                        PackageName = "Cake"
                    }
                }
                }, out cakeVersion);

            // Build the DSL model.
            var dslModel = DslModelBuilder.Build(documentModel);

            // Generate packages.config content.
            var packagesConfig = new PackagesConfigContent(cakeVersion);

            // Build the container.
            var builder = new ContainerBuilder();
            builder.RegisterModule<CoreModule>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(documentModel).As<DocumentModel>().SingleInstance();
            builder.RegisterInstance(dslModel).As<DslModel>().SingleInstance();
            builder.RegisterType<DocumentModelResolver>().SingleInstance();
            builder.RegisterType<RouteService>().SingleInstance();
            builder.RegisterType<UrlResolver>().As<IUrlResolver>().As<UrlResolver>().SingleInstance();
            builder.RegisterType<SignatureCache>().SingleInstance();
            builder.RegisterType<ApiServices>().SingleInstance();
            builder.RegisterType<LanguageProvider>().SingleInstance();
            builder.RegisterType<SyntaxRenderer>().SingleInstance();
            builder.RegisterType<SignatureRenderer>().SingleInstance();
            builder.RegisterType<ApiServices>().SingleInstance();
            builder.RegisterInstance(packagesConfig).SingleInstance();
            var container = builder.Build();

            // Read the topics and register.
            var reader = container.Resolve<ITopicReader>();
            var topics = reader.Read(appDataPath.CombineWithFilePath("docs.xml"));

            // Read all blog entries.
            var blogReader = container.Resolve<IBlogReader>();
            var blogIndex = blogReader.Parse(appDataPath.Combine("blog"));

            // Read all addins.
            var addinReader = container.Resolve<IAddinReader>();
            var addins = addinReader.Read(appDataPath.CombineWithFilePath("addins.xml"));

            // Update the container.
            builder = new ContainerBuilder();
            builder.RegisterInstance(topics).As<TopicTree>().SingleInstance();
            builder.RegisterInstance(blogIndex).As<BlogIndex>().SingleInstance();
            builder.RegisterInstance(addins).As<AddinIndex>().SingleInstance();
            builder.Update(container);

            // Perform registrations.
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
