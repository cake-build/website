using System;
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

        public List<DocumentedNamespace> FindNamespacesFromRoutePart(string id)
        {
            if (!_namespaces.ContainsKey(id))
            {
                throw new InvalidOperationException("Could not find namespace.");
            }
            return _namespaces[id];
        }

        public DocumentedType FindTypeFromRoutePart(string id)
        {
            if (!_types.ContainsKey(id))
            {
                throw new InvalidOperationException("Could not find type.");
            }
            return _types[id];
        }

        public DocumentedMember FindTypeMemberFromRoutePart(string id)
        {
            if (!_members.ContainsKey(id))
            {
                throw new InvalidOperationException("Could not find member.");
            }
            return _members[id];
        }

        public string GetRoutePart(DocumentedNamespace @namespace)
        {
            return @namespace.Identity.ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedType type)
        {
            return string.Format("{0:X8}", type.Identity.GetHashCode()).ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedMethod method)
        {
            return string.Format("{0:X8}", method.Identity.GetHashCode()).ToLowerInvariant();
        }

        public string GetRoutePart(DocumentedProperty property)
        {
            return string.Format("{0:X8}", property.Identity.GetHashCode()).ToLowerInvariant();
        }
    }
}
