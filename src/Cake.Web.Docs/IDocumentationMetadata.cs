using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Web.Docs
{
    public interface IDocumentationMetadata
    {
        bool IsExternallyOwned { get; }
    }
}
