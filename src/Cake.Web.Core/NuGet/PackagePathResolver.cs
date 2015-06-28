using Cake.Core.IO;
using NuGet;

namespace Cake.Web.Core.NuGet
{
    internal sealed class PackagePathResolver : IPackagePathResolver
    {
        private readonly DirectoryPath _root;

        public PackagePathResolver(DirectoryPath root)
        {
            _root = root;
        }

        public string GetInstallPath(IPackage package)
        {
            return _root.Combine(GetPackageDirectory(package)).FullPath;
        }

        public string GetPackageDirectory(IPackage package)
        {
            return GetPackageDirectory(package.Id, package.Version);
        }

        public string GetPackageDirectory(string packageId, SemanticVersion version)
        {
            return packageId;
        }

        public string GetPackageFileName(IPackage package)
        {
            return GetPackageFileName(package.Id, package.Version);
        }

        public string GetPackageFileName(string packageId, SemanticVersion version)
        {
            var fileNameBase = packageId + "." + version;
            return fileNameBase + Constants.PackageExtension;
        }
    }
}