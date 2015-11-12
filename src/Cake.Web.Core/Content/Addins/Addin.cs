using System;
using System.Collections.Generic;
using Cake.Web.Core.NuGet;
using Cake.Web.Docs;

namespace Cake.Web.Core.Content.Addins
{
    public sealed class Addin : IDocumentationMetadata
    {
        public string Name { get; set; }
        public Uri Website { get; set; }
        public Uri Repository { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
        public PackageDefinition PackageDefinition { get; set; }

        public Uri Uri => Repository ?? Website;
        public bool IsExternallyOwned => true;
        public bool IsAlias => false;
        public bool IsPropertyAlias => false;

        public Addin()
        {
            Categories = new List<string>();
        }
    }
}
