using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class AssemblyViewModel
    {
        public string Name { get; }
        public IDocumentationMetadata Metadata { get; }
        public IReadOnlyList<DocumentedNamespace> Namespaces { get; }

        public AssemblyViewModel(DocumentedAssembly assembly)
        {
            Metadata = assembly.Metadata;
            Name = assembly.Name;
            Namespaces = new List<DocumentedNamespace>(assembly.Namespaces.Where(ns => ns.HasContent));
        }
    }
}