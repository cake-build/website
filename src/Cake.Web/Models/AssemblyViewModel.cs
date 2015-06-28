using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;

namespace Cake.Web.Models
{
    public sealed class AssemblyViewModel
    {
        private readonly string _name;
        private readonly List<DocumentedNamespace> _namespaces;

        public string Name
        {
            get { return _name; }
        }

        public IReadOnlyList<DocumentedNamespace> Namespaces
        {
            get { return _namespaces; }
        }

        public AssemblyViewModel(DocumentedAssembly assembly)
        {
            _name = assembly.Name;
            _namespaces = new List<DocumentedNamespace>(assembly.Namespaces.Where(ns => ns.HasContent));
        }
    }
}