using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Docs
{
    public sealed class CakeDocumentationMetadata : IDocumentationMetadata
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

        public CakeDocumentationMetadata()
        {
            _uri = new Uri("http://cakebuild.net");
        }
    }
}
