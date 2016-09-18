using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Core.IO;

namespace Cake.Web.Core.NuGet
{
    public sealed class NuGetInstaller
    {
        private readonly IFileSystem _fileSystem;
        private readonly IGlobber _globber;

        public NuGetInstaller(IFileSystem fileSystem, IGlobber globber)
        {
            _fileSystem = fileSystem;
            _globber = globber;
        }

        public IEnumerable<IFile> Install(PackageDefinition package, DirectoryPath root)
        {
            var result = new List<FilePath>();
            var paths = new FilePathCollection(PathComparer.Default);

            // InstallPackage the package.
            var packagePath = InstallPackage(package, root);
            var packageDirectory = _fileSystem.GetDirectory(packagePath);

            if (package.Filters != null && package.Filters.Count > 0)
            {
                // Get all files matching the filters.
                foreach (var filter in package.Filters)
                {
                    var pattern = string.Concat(packagePath.FullPath, "/", filter.TrimStart('/', '\\'));
                    paths.Add(_globber.GetFiles(pattern));
                }
            }
            else
            {
                // Do a recursive search in the package directory.
                paths.Add(packageDirectory.
                    GetFiles("*", SearchScope.Recursive)
                    .Select(file => file.Path));
            }

            if (paths.Count > 0)
            {
                result.AddRange(paths);
            }

            return result.Select(path => _fileSystem.GetFile(path));
        }

        public DirectoryPath InstallPackage(PackageDefinition package, DirectoryPath root)
        {
            var packagePath = root.Combine("libs");
            if (!_fileSystem.Exist(packagePath))
            {
                _fileSystem.GetDirectory(packagePath).Create();
            }

            if (_fileSystem.Exist(packagePath.Combine(package.PackageName)))
            {
                return packagePath.Combine(package.PackageName);
            }

            var arguments = $"install \"{package.PackageName}\" -Source \"https://api.nuget.org/v3/index.json\" -PreRelease -ExcludeVersion -OutputDirectory \"{packagePath.FullPath}\"{(!string.IsNullOrWhiteSpace(package.Version) ? $" -Version \"{package.Version}\"" : string.Empty)}";
            var fallbackarguments = $"install \"{package.PackageName}\" -Source \"https://api.nuget.org/v3/index.json\" -Source \"https://www.myget.org/F/xunit/api/v3/index.json\" -Source \"https://dotnet.myget.org/F/dotnet-core/api/v3/index.json\" -Source \"https://dotnet.myget.org/F/cli-deps/api/v3/index.json\" -PreRelease -ExcludeVersion -OutputDirectory \"{packagePath.FullPath}\"{(!string.IsNullOrWhiteSpace(package.Version) ? $" -Version \"{package.Version}\"" : string.Empty)}";
            
            ExecuteNuget(arguments, fallbackarguments, 3);

            // Return the installation directory.
            return packagePath.Combine(package.PackageName);
        }

        private static void ExecuteNuget(string arguments, string fallbackarguments, int retries)
        {
            while (true)
            {
                var process = Process.Start(new ProcessStartInfo("nuget", arguments) {UseShellExecute = false});
                if (process == null)
                {
                    throw new NullReferenceException(nameof(process));
                }

                bool timeout;
                if ((timeout = !process.WaitForExit(45000)) && !process.HasExited)
                {
                    process.Kill();
                }

                if (!timeout && process.ExitCode == 0)
                {
                    return;
                }

                if (--retries <= 0)
                {
                    throw new InvalidOperationException($"Failed to install package ({arguments})");
                }

                arguments = fallbackarguments;
            }
        }
    }
}
