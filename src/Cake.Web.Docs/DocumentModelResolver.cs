using System;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Utility used for resolving documentation.
    /// </summary>
    public class DocumentModelResolver
    {
        private readonly DocumentModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModelResolver"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public DocumentModelResolver(DocumentModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Finds a namespace by it's cref.
        /// </summary>
        /// <param name="cref">The cref.</param>
        /// <returns>The documented namespace, or <c>null</c> if not found.</returns>
        public DocumentedNamespace FindNamespace(string cref)
        {
            foreach (var assembly in _model.Assemblies)
            {
                foreach (var @namespace in assembly.Namespaces)
                {
                    if (@namespace.Identity == cref)
                    {
                        return @namespace;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a type by it's cref.
        /// </summary>
        /// <param name="cref">The cref.</param>
        /// <returns>
        /// The documented type, or <c>null</c> if not found.
        /// </returns>
        public virtual DocumentedType FindType(string cref)
        {
            foreach (var assembly in _model.Assemblies)
            {
                foreach (var @namespace in assembly.Namespaces)
                {
                    foreach (var type in @namespace.Types)
                    {
                        if (type.Identity.Equals(cref, StringComparison.Ordinal))
                        {
                            return type;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a method by it's cref.
        /// </summary>
        /// <param name="cref">The cref.</param>
        /// <returns>
        /// The documented method, or <c>null</c> if not found.
        /// </returns>
        public virtual DocumentedMethod FindMethod(string cref)
        {
            foreach (var assembly in _model.Assemblies)
            {
                foreach (var @namespace in assembly.Namespaces)
                {
                    foreach (var type in @namespace.Types)
                    {
                        foreach (var method in type.Methods)
                        {
                            if (method.Identity.Equals(cref, StringComparison.Ordinal))
                            {
                                return method;
                            }
                        }
                        foreach (var method in type.Operators)
                        {
                            if (method.Identity.Equals(cref, StringComparison.Ordinal))
                            {
                                return method;
                            }
                        }
                        foreach (var method in type.Constructors)
                        {
                            if (method.Identity.Equals(cref, StringComparison.Ordinal))
                            {
                                return method;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
