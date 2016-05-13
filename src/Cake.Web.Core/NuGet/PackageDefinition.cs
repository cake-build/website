using System.Collections.Generic;
using Cake.Web.Docs;

namespace Cake.Web.Core.NuGet
{
    public sealed class PackageDefinition
    {
        public string PackageName { get; set; }
        public string Version { get; set; }
        public List<string> Filters { get; set; }
        public IDocumentationMetadata Metadata { get; set; }

        public PackageDefinition()
        {
            Filters = new List<string>();
        }
    }
}