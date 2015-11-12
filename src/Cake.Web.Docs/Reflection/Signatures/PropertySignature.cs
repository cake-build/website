namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a property signature.
    /// </summary>
    public sealed class PropertySignature
    {
        /// <summary>
        /// Gets the Identity.
        /// </summary>
        /// <value>The Identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>The property name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the declaring type.
        /// </summary>
        /// <value>The declaring type.</value>
        public TypeSignature DeclaringType { get; }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value>The return type.</value>
        public TypeSignature PropertyType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySignature"/> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="propertyType">The property type.</param>
        public PropertySignature(
            string identity,
            string name,
            TypeSignature declaringType,
            TypeSignature propertyType)
        {
            Identity = identity;
            Name = name;
            DeclaringType = declaringType;
            PropertyType = propertyType;
        }
    }
}
