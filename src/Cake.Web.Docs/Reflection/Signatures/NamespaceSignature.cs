namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents the name of a namespace.
    /// </summary>
    public sealed class NamespaceSignature
    {
        private readonly string _name;
        private readonly string _identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceSignature" /> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        public NamespaceSignature(string identity)
        {
            _identity = identity;
            _name = identity;            
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
    }
}