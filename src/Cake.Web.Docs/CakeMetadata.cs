using System;

namespace Cake.Web.Docs
{
    public sealed class CakeMetadata : IDocumentationMetadata
    {
        public Uri Uri { get; }

        public bool IsExternallyOwned => false;
        public bool IsAlias => false;
        public bool IsPropertyAlias => false;

        public CakeMetadata()
        {
            Uri = new Uri("http://cakebuild.net");
        }
    }
}
