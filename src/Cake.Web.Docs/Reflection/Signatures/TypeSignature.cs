using System.Collections.Generic;

namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents the name of a type.
    /// </summary>
    public sealed class TypeSignature
    {
        /// <summary>
        /// Gets the Identity.
        /// </summary>
        /// <value>The Identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public NamespaceSignature Namespace { get; }

        /// <summary>
        /// Gets the generic arguments.
        /// </summary>
        /// <value>The generic arguments.</value>
        public IReadOnlyList<string> GenericArguments { get; }

        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>The generic parameters.</value>
        public IReadOnlyList<TypeSignature> GenericParameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignature" /> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The type URL.</param>
        /// <param name="namespace">The namespace.</param>
        /// <param name="genericArguments">The generic arguments.</param>
        /// <param name="genericParameters">The generic parameters.</param>
        public TypeSignature(
            string identity,
            string name,
            string url,
            NamespaceSignature @namespace,
            IEnumerable<string> genericArguments,
            IEnumerable<TypeSignature> genericParameters)
        {
            Name = name;
            Url = url ?? "#unresolved";
            Identity = identity;
            Namespace = @namespace;
            GenericArguments = new List<string>(genericArguments);
            GenericParameters = new List<TypeSignature>(genericParameters);
        }
    }
}
