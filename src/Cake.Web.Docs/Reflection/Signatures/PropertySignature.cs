namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a property signature.
    /// </summary>
    public sealed class PropertySignature
    {
        private readonly string _identity;
        private readonly string _name;
        private readonly TypeSignature _declaringType;
        private readonly TypeSignature _propertyType;

        /// <summary>
        /// Gets the Identity.
        /// </summary>
        /// <value>The Identity.</value>
        public string Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        /// <value>The property name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the declaring type.
        /// </summary>
        /// <value>The declaring type.</value>
        public TypeSignature DeclaringType
        {
            get { return _declaringType; }
        }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value>The return type.</value>
        public TypeSignature PropertyType
        {
            get { return _propertyType; }
        }

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
            _identity = identity;
            _name = name;
            _declaringType = declaringType;
            _propertyType = propertyType;
        }
    }
}
