using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Models
{
    public sealed class NamespaceViewModel
    {
        public string Name { get; }
        public DocumentedNamespace Data { get; }
        public List<DocumentedNamespace> Namespaces { get; }
        public List<DocumentedType> Classes { get; }
        public List<DocumentedType> Interfaces { get; }
        public SummaryComment Summary { get; }

        public NamespaceViewModel(IReadOnlyList<DocumentedNamespace> namespaces)
        {
            if (namespaces.Count == 0)
            {
                throw new ArgumentException("No namespaces in list.");
            }

            Data = namespaces[0];
            Name = Data.Name;

            var namespaceWithSummary = namespaces.FirstOrDefault(x => x.Summary != null);
            if (namespaceWithSummary != null)
            {
                Summary = namespaceWithSummary.Summary;
            }

            Classes = new List<DocumentedType>();
            Interfaces = new List<DocumentedType>();
            foreach (var @namespace in namespaces)
            {
                var classes = @namespace.Types.Where(x => x.Definition.IsClass && !IsExtensionMethodClass(x)).ToArray();
                Classes.AddRange(classes);

                var interfaces = @namespace.Types.Where(x => x.Definition.IsInterface).ToArray();
                Interfaces.AddRange(interfaces);
            }

            // For child namespaces, just get them from the first one
            // since they're going to be the same anyway.
            Namespaces = new List<DocumentedNamespace>();
            foreach (var childNamespace in Data.Tree.Children)
            {
                Namespaces.Add(childNamespace.Namespace);
            }
        }

        private static bool IsExtensionMethodClass(DocumentedType type)
        {
            // TODO: Cache this
            if (!type.Identity.Contains("Alias"))
            {
                return type.Definition.IsClass && type.Definition.IsStatic() &&
                       type.Methods.All(x => x.MethodClassification == MethodClassification.ExtensionMethod);
            }
            return false;
        }
    }
}