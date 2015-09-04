using Autofac;
using Cake.Core;
using Cake.Core.IO;
using Cake.Web.Core.Content;
using Cake.Web.Core.Content.Addins;
using Cake.Web.Core.Content.Blog;
using Cake.Web.Core.Content.Documentation;

namespace Cake.Web.Core
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register core services.
            builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
            builder.RegisterType<CakeEnvironment>().As<ICakeEnvironment>().SingleInstance();

            // Register content services.
            builder.RegisterType<ContentConverter>();
            builder.RegisterType<ContentParser>();
            builder.RegisterType<ContentProcessor>();

            // Register blog services.
            builder.RegisterType<BlogReader>().As<IBlogReader>().SingleInstance();

            // Register documentation services.
            builder.RegisterType<TopicReader>().As<ITopicReader>().SingleInstance();

            // Register addin services.
            builder.RegisterType<AddinReader>().As<IAddinReader>().SingleInstance();
        }
    }
}
