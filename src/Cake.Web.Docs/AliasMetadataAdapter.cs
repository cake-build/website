using System;

namespace Cake.Web.Docs
{
    public sealed class AliasMetadataAdapter : IDocumentationMetadata
    {
        private readonly IDocumentationMetadata _metadata;

        public bool IsPropertyAlias { get; }

        public Uri Uri => _metadata.Uri;
        public bool IsExternallyOwned => _metadata.IsExternallyOwned;
        public bool IsAlias => true;

        public AliasMetadataAdapter(IDocumentationMetadata metadata, bool isPropertyAlias)
        {
            _metadata = metadata;
            IsPropertyAlias = isPropertyAlias;
        }
    }
}