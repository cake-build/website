using System;

namespace Cake.Web.Docs
{
    public interface IDocumentationMetadata
    {
        Uri Uri { get; }
        bool IsExternallyOwned { get; }
        bool IsAlias { get; }
        bool IsPropertyAlias { get; }
    }
}
