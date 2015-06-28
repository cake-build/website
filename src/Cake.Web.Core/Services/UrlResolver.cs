using System.Collections.Generic;
using Cake.Web.Docs;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Core.Services
{
    public sealed class UrlResolver : IUrlResolver
    {
        private readonly RouteService _service;
        private readonly Dictionary<string, string> _routeLookup;

        public UrlResolver(DocumentModel model, RouteService service)
        {
            _service = service;
            _routeLookup = new Dictionary<string, string>();

            // Assemblies
            foreach (var assembly in model.Assemblies)
            {
                // Namespaces
                foreach (var @namespace in assembly.Namespaces)
                {
                    if (!_routeLookup.ContainsKey(@namespace.Identity))
                    {
                        // Namespaces are tricky since they might exist in multiple assemblies.
                        _routeLookup.Add(@namespace.Identity, GetUrl(@namespace));
                    }

                    // Types
                    foreach (var type in @namespace.Types)
                    {
                        _routeLookup.Add(type.Identity, GetUrl(type));

                        // Constructors
                        foreach (var method in type.Constructors)
                        {
                            _routeLookup.Add(method.Identity, GetUrl(method));
                        }
                        // Methods
                        foreach (var method in type.Methods)
                        {
                            _routeLookup.Add(method.Identity, GetUrl(method));
                        }
                        // Operators
                        foreach (var method in type.Operators)
                        {
                            _routeLookup.Add(method.Identity, GetUrl(method));
                        }
                        // Properties
                        foreach (var property in type.Properties)
                        {
                            _routeLookup.Add(property.Identity, GetUrl(property));
                        }
                    }
                }
            }
        }

        public string GetUrl(string cref)
        {
            if (_routeLookup.ContainsKey(cref))
            {
                return string.Concat("/api/", _routeLookup[cref]);
            }
            return null;
        }

        private string GetUrl(DocumentedNamespace ns)
        {
            return _service.GetRoutePart(ns);
        }

        private string GetUrl(DocumentedType type)
        {
            var nsRoute = _service.GetRoutePart(type.Namespace);
            var typeRoute = _service.GetRoutePart(type);
            return string.Concat(nsRoute, "/", typeRoute);
        }

        private string GetUrl(DocumentedMethod method)
        {
            var nsRoute = _service.GetRoutePart(method.Type.Namespace);
            var typeRoute = _service.GetRoutePart(method.Type);
            var methodRoute = _service.GetRoutePart(method);
            return string.Concat(nsRoute, "/", typeRoute, "/", methodRoute);
        }

        private string GetUrl(DocumentedProperty property)
        {
            var nsRoute = _service.GetRoutePart(property.Type.Namespace);
            var typeRoute = _service.GetRoutePart(property.Type);
            var propertyRoute = _service.GetRoutePart(property);
            return string.Concat(nsRoute, "/", typeRoute, "/", propertyRoute);
        }
    }
}