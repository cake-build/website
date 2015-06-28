namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a parameter signature.
    /// </summary>
    public sealed class ParameterSignature
    {
        private readonly string _name;
        private readonly TypeSignature _parameterType;
        private readonly bool _isOutParameter;
        private readonly bool _isRefParameter;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the parameter type.
        /// </summary>
        /// <value>The parameter type.</value>
        public TypeSignature ParameterType
        {
            get { return _parameterType; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an out parameter.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an out parameter; otherwise, <c>false</c>.
        /// </value>
        public bool IsOutParameter
        {
            get { return _isOutParameter; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a reference parameter.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a reference parameter; otherwise, <c>false</c>.
        /// </value>
        public bool IsRefParameter
        {
            get { return _isRefParameter; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterSignature" /> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="parameterType">The parameter type.</param>
        /// <param name="isOutParameter">if set to <c>true</c>, this is an out parameter.</param>
        /// <param name="isRefParameter">if set to <c>true</c>, this is a reference parameter.</param>
        public ParameterSignature(string name, TypeSignature parameterType, bool isOutParameter, bool isRefParameter)
        {
            _name = name;
            _parameterType = parameterType;
            _isOutParameter = isOutParameter;
            _isRefParameter = isRefParameter;
        }
    }
}