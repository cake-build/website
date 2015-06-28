using System.Collections.Generic;
using System.Linq;
using Cake.Core.IO;
using NuGet;
using IFileSystem = Cake.Core.IO.IFileSystem;

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

        public IEnumerable<IFile> Install(NuGetConfiguration options, DirectoryPath root, bool installAddins)
        {
            var result = new List<FilePath>();
            foreach (var package in options.Packages)
            {
                var paths = new FilePathCollection(PathComparer.Default);

                // Install the package.
                var packagePath = Install(package, root);
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
            }

            return result.Select(path => _fileSystem.GetFile(path));
        }

        public DirectoryPath Install(PackageDefinition package, DirectoryPath root)
        {
            var packagePath = root.Combine("libs");
            if (!_fileSystem.Exist(packagePath))
            {
                _fileSystem.GetDirectory(packagePath).Create();
            }

            if (!_fileSystem.Exist(packagePath.Combine(package.PackageName)))
            {
                var packageManager = CreatePackageManager(packagePath);
                packageManager.InstallPackage(package.PackageName);
            }

            // Return the installation directory.
            return packagePath.Combine(package.PackageName);
        }

        private static PackageManager CreatePackageManager(DirectoryPath packagePath)
        {
            var repository = PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
            var resolver = new PackagePathResolver(packagePath);
            var fileSystem = new PhysicalFileSystem(packagePath.FullPath);
            return new PackageManager(repository, resolver, fileSystem);
        }
    }
}
