using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Web.Core.NuGet;
using Cake.Web.Docs;

namespace Cake.Web
{
    public class NuGetBootstrapper
    {
        public static DocumentModel Download(DirectoryPath appDataPath, NuGetConfiguration configuration)
        {
            var files = Install(configuration, appDataPath);
            var builder = new DocumentModelBuilder();
            return builder.BuildModel(files.Select(x => x.Path.FullPath));
        }

        private static IEnumerable<IFile> Install(NuGetConfiguration configuration, DirectoryPath appDataPath)
        {
            var fileSystem = new FileSystem();
            var environment = new CakeEnvironment();
            var globber = new Globber(fileSystem, environment);

            var installer = new NuGetInstaller(fileSystem, globber);
            return installer.Install(configuration, appDataPath, true);
        }
    }
}