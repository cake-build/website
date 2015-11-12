namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a parameter signature.
    /// </summary>
    public sealed class ParameterSignature
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the parameter type.
        /// </summary>
        /// <value>The parameter type.</value>
        public TypeSignature ParameterType { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is an out parameter.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an out parameter; otherwise, <c>false</c>.
        /// </value>
        public bool IsOutParameter { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a reference parameter.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a reference parameter; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefParameter { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterSignature" /> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="parameterType">The parameter type.</param>
        /// <param name="isOutParameter">if set to <c>true</c>, this is an out parameter.</param>
        /// <param name="isRefParameter">if set to <c>true</c>, this is a reference parameter.</param>
        public ParameterSignature(string name, TypeSignature parameterType, bool isOutParameter, bool isRefParameter)
        {
            Name = name;
            ParameterType = parameterType;
            IsOutParameter = isOutParameter;
            IsRefParameter = isRefParameter;
        }
    }
}