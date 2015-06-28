using System.Collections.Generic;

namespace Cake.Web.Core.NuGet
{
    public sealed class PackageDefinition
    {
        public string PackageName { get; set; }
        public List<string> Filters { get; set; }
    }
}