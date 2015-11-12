namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents the name of a namespace.
    /// </summary>
    public sealed class NamespaceSignature
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
        /// Initializes a new instance of the <see cref="NamespaceSignature" /> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        public NamespaceSignature(string identity)
        {
            Identity = identity;
            Name = identity;
        }
    }
}