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
        private readonly string _name;
        private readonly SummaryComment _summary;
        private readonly DocumentedNamespace _data;
        private readonly List<DocumentedNamespace> _namespaces;
        private readonly List<DocumentedType> _classes;
        private readonly List<DocumentedType> _interfaces;

        public string Name
        {
            get { return _name; }
        }

        public DocumentedNamespace Data
        {
            get { return _data; }
        }

        public List<DocumentedNamespace> Namespaces
        {
            get { return _namespaces; }
        }

        public List<DocumentedType> Classes
        {
            get { return _classes; }
        }

        public List<DocumentedType> Interfaces
        {
            get { return _interfaces; }
        }

        public SummaryComment Summary
        {
            get { return _summary; }
        }

        public NamespaceViewModel(IReadOnlyList<DocumentedNamespace> namespaces)
        {
            if (namespaces.Count == 0)
            {
                throw new ArgumentException("No namespaces in list.");
            }

            _data = namespaces[0];
            _name = _data.Name;

            var namespaceWithSummary = namespaces.FirstOrDefault(x => x.Summary != null);
            if (namespaceWithSummary != null)
            {
                _summary = namespaceWithSummary.Summary;
            }

            _classes = new List<DocumentedType>();
            _interfaces = new List<DocumentedType>();
            foreach (var @namespace in namespaces)
            {
                var classes = @namespace.Types.Where(x => x.Definition.IsClass && !IsExtensionMethodClass(x)).ToArray();
                _classes.AddRange(classes);

                var interfaces = @namespace.Types.Where(x => x.Definition.IsInterface).ToArray();
                _interfaces.AddRange(interfaces);
            }

            // For child namespaces, just get them from the first one 
            // since they're going to be the same anyway.
            _namespaces = new List<DocumentedNamespace>();
            foreach (var childNamespace in _data.Tree.Children)
            {
                _namespaces.Add(childNamespace.Namespace);
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