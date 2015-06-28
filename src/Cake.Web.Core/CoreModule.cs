using Autofac;
using Cake.Core;
using Cake.Core.IO;
using Cake.Web.Core.Documentation;
using Cake.Web.Core.Documentation.Processing;

namespace Cake.Web.Core
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register core services.
            builder.RegisterType<FileSystem>().As<IFileSystem>().SingleInstance();
            builder.RegisterType<CakeEnvironment>().As<ICakeEnvironment>().SingleInstance();

            // Register documentation services.
            builder.RegisterType<ContentConverter>();
            builder.RegisterType<ContentParser>();
            builder.RegisterType<ContentProcessor>();
            builder.RegisterType<TopicReader>().As<ITopicReader>().SingleInstance();
        }
    }
}
