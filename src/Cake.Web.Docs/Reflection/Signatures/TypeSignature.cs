using System.Collections.Generic;

namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents the name of a type.
    /// </summary>
    public sealed class TypeSignature
    {
        private readonly string _identity;
        private readonly string _name;
        private readonly string _url;
        private readonly NamespaceSignature _namespace;
        private readonly List<string> _genericArguments;
        private readonly List<TypeSignature> _genericParameters;

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
            _name = name;
            _url = url ?? "#unresolved";
            _identity = identity;
            _namespace = @namespace;
            _genericArguments = new List<string>(genericArguments);
            _genericParameters = new List<TypeSignature>(genericParameters);
        }

        /// <summary>
        /// Gets the Identity.
        /// </summary>
        /// <value>The Identity.</value>
        public string Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public NamespaceSignature Namespace
        {
            get { return _namespace; }
        }

        /// <summary>
        /// Gets the generic arguments.
        /// </summary>
        /// <value>The generic arguments.</value>
        public List<string> GenericArguments
        {
            get { return _genericArguments; }
        }

        /// <summary>
        /// Gets the generic parameters.
        /// </summary>
        /// <value>The generic parameters.</value>
        public IReadOnlyList<TypeSignature> GenericParameters
        {
            get { return _genericParameters; }
        }
    }
}
