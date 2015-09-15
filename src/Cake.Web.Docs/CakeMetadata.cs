using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Docs
{
    public sealed class AliasMetadataAdapter : IDocumentationMetadata
    {
        private readonly IDocumentationMetadata _metadata;
        private readonly bool _isPropertyAlias;

        public AliasMetadataAdapter(IDocumentationMetadata metadata, bool isPropertyAlias)
        {
            _metadata = metadata;
            _isPropertyAlias = isPropertyAlias;
        }

        public Uri Uri
        {
            get { return _metadata.Uri; }
        }

        public bool IsExternallyOwned
        {
            get { return _metadata.IsExternallyOwned; }
        }

        public bool IsAlias
        {
            get { return true; }
        }

        public bool IsPropertyAlias
        {
            get { return _isPropertyAlias; }
        }
    }

    public sealed class CakeMetadata : IDocumentationMetadata
    {
        private readonly Uri _uri;

        public Uri Uri
        {
            get { return _uri; }
        }

        public bool IsExternallyOwned
        {
            get { return false; }
        }

        public bool IsAlias
        {
            get { return false; }
        }

        public bool IsPropertyAlias
        {
            get { return false; }
        }

        public CakeMetadata()
        {
            _uri = new Uri("http://cakebuild.net");
        }
    }
}
