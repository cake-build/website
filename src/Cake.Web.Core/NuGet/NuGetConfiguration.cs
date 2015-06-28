using System.Collections.Generic;

namespace Cake.Web.Core.NuGet
{
    public sealed class NuGetConfiguration
    {
        public List<PackageDefinition> Packages { get; set; }
    }
}