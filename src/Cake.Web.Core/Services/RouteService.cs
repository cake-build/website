using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs;

namespace Cake.Web.Core.Services
{
    public sealed class RouteService
    {
        private readonly Dictionary<string, List<DocumentedNamespace>> _namespaces;
        private readonly Dictionary<string, DocumentedType> _types;
        private readonly Dictionary<string, DocumentedMember> _members;

        public RouteService(DocumentModel model)
        {
            _namespaces = new Dictionary<string, List<DocumentedNamespace>>();
            _types = new Dictionary<string, DocumentedType>();
            _members = new Dictionary<string, DocumentedMember>();

            // Cache information for fast lookup.
            foreach (var @namespace in model.Assemblies.SelectMany(assembly => assembly.Namespaces))
            {
                // Namespace
                var namespaceRoute = GetRoutePart(@namespace);
                if (!_namespaces.ContainsKey(namespaceRoute))
                {
                    _namespaces.Add(namespaceRoute, new List<DocumentedNamespace>());
                }
                _namespaces[namespaceRoute].Add(@namespace);

                // Types
                foreach (var type in @namespace.Types)
                {
                    _types.Add(GetRoutePart(type), type);

                    // Constructors
                    foreach (var constructor in type.Constructors)
                    {
                        _members.Add(GetRoutePart(constructor), constructor);
                    }
                    // Methods
                    foreach (var method in type.Methods)
                    {
                        _members.Add(GetRoutePart(method), method);
                    }
                    // Operators
                    foreach (var @operator in type.Operators)
                    {
                        _members.Add(GetRoutePart(@operator), @operator);
                    }
                    // Properties
                    foreach (var property in type.Properties)
                    {
                        _members.Add(GetRoutePart(property), property);
                    }
                }
            }
        }

        public bool TryFindNamespacesFromRoutePart(string id, out List<DocumentedNamespace> namespaces)
        {
            return _namespaces.TryGetValue(id, out namespaces);
        }

        public bool TryFindTypeFromRoutePart(string id, out DocumentedType type)
        {
            return _types.TryGetValue(id, out type);
        }

        public bool TryFindTypeMemberFromRoutePart(string id, out DocumentedMember member)
        {
            return _members.TryGetValue(id, out member);
        }

        public string GetRoutePart(DocumentedNamespace @namespace)
        {
            return @namespace.Identity.ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedType type)
        {
            return $"{type.Identity.GetHashCode():X8}".ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedMethod method)
        {
            return $"{method.Identity.GetHashCode():X8}".ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedProperty property)
        {
            return $"{property.Identity.GetHashCode():X8}".ToLowerInvariant();
        }
    }
}
