using System;
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
        public static DocumentModel Download(DirectoryPath appDataPath, NuGetConfiguration configuration, out string version)
        {
            var files = Install(configuration, appDataPath);
            var builder = new DocumentModelBuilder();

            // Find Cake.exe.
            version = "0.5.2"; // Default to this version if we could not find.
            var exe = files.FirstOrDefault(x => x.Path.FullPath.EndsWith("Cake.Core.dll"));
            if (exe != null)
            {
                var name = AssemblyName.GetAssemblyName(exe.Path.FullPath);
                if (name != null)
                {
                    version = string.Format("{0}.{1}.{2}",
                        name.Version.Major, name.Version.Minor,
                        name.Version.Build);
                }
            }

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