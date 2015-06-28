using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Docs
{
    internal sealed class ExtensionMethodFinder
    {
        private readonly Dictionary<string, HashSet<DocumentedMethod>> _lookup;

        public ExtensionMethodFinder(DocumentModel model)
        {
            _lookup = new Dictionary<string, HashSet<DocumentedMethod>>(StringComparer.Ordinal);

            // Find all extension methods.
            foreach (var method in model.Assemblies
                .SelectMany(assembly => assembly.Namespaces)
                .SelectMany(ns => ns.Types)
                .SelectMany(type => type.Methods)
                .Where(method => method.MethodClassification == MethodClassification.ExtensionMethod))
            {
                try
                {
                    var parameterType = method.Parameters[0].Definition.ParameterType.Resolve();
                    if (!_lookup.ContainsKey(parameterType.FullName))
                    {
                        _lookup.Add(parameterType.FullName, new HashSet<DocumentedMethod>());
                    }
                    _lookup[parameterType.FullName].Add(method);
                }
                catch (Exception)
                {
                    // Resolution failed.
                }
            }
        }

        public List<DocumentedMethod> FindExtensionMethods(DocumentedType type)
        {
            var methods = new List<DocumentedMethod>();
            var current = type.Definition;
            while (current != null)
            {
                if (current.FullName == "System.Object")
                {
                    // Skip extension methods for System.Object.
                    break;
                }

                // Found extension methods for the current type?
                if (_lookup.ContainsKey(current.FullName))
                {
                    methods.AddRange(_lookup[current.FullName]);
                }

                // Found extension methods for the current types interfaces?
                foreach (var typeReference in current.Interfaces)
                {
                    if (_lookup.ContainsKey(typeReference.FullName))
                    {
                        methods.AddRange(_lookup[typeReference.FullName]);
                    }
                }

                // Get the base type.
                var baseType = current.BaseType;
                if (baseType != null)
                {
                    try
                    {
                        // Resolve the type.
                        current = baseType.Resolve();
                    }
                    catch (Exception)
                    {
                        // Resolution failed.
                        current = null;
                    }
                }
                else
                {
                    current = null;
                }
            }
            return methods;
        }
    }
}
