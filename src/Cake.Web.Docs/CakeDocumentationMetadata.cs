using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Docs
{
    public sealed class CakeDocumentationMetadata : IDocumentationMetadata
    {
        public bool IsExternallyOwned
        {
            get { return false; }
        }
    }
}
