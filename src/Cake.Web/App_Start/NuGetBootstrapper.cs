using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cake.Core;
using Cake.Core.IO;
using Cake.Web.Core.NuGet;
using Cake.Web.Docs;

namespace Cake.Web
{
    public class NuGetBootstrapper
    {
        public static DocumentModel Download(
            DirectoryPath appDataPath,
            NuGetConfiguration configuration,
            out string version)
        {
            var fileSystem = new FileSystem();
            var environment = new CakeEnvironment();
            var globber = new Globber(fileSystem, environment);
            var installer = new NuGetInstaller(fileSystem, globber);

            var files = new Dictionary<string, IDocumentationMetadata>();
            foreach (var package in configuration.Packages)
            {
                var packageFiles = installer.Install(package, appDataPath);
                foreach (var packageFile in packageFiles)
                {
                    files.Add(packageFile.Path.FullPath, package.Metadata);
                }
            }

            // Find Cake.exe.
            version = "0.5.2"; // Default to this version if we could not find.
            var exe = files.Keys.FirstOrDefault(x => x.EndsWith("Cake.Core.dll"));
            if (exe != null)
            {
                var name = AssemblyName.GetAssemblyName(exe);
                if (name != null)
                {
                    version = $"{name.Version.Major}.{name.Version.Minor}.{name.Version.Build}";
                }
            }

            // Build the model.
            return new DocumentModelBuilder()
                .BuildModel(files);
        }
    }
}