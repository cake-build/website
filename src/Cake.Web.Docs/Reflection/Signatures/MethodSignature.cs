using System.Collections.Generic;

namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a method signature.
    /// </summary>
    public sealed class MethodSignature
    {
        private readonly string _identity;
        private readonly string _name;
        private readonly string _url;
        private readonly TypeSignature _returnType;
        private readonly TypeSignature _declaringType;
        private readonly List<string> _genericParameters;
        private readonly List<ParameterSignature> _parameters;
        private readonly MethodClassification _classification;
        private readonly OperatorClassification _operatorClassification;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignature" /> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        /// <param name="name">The type name.</param>
        /// <param name="url">The url.</param>
        /// <param name="classification">The method classification.</param>
        /// <param name="operatorClassification">The operator classification.</param>
        /// <param name="declaringType">The declaring type.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="genericParameters">The method's generic parameters.</param>
        /// <param name="parameters">The parameters.</param>
        public MethodSignature(
            string identity,
            string name,
            string url,
            MethodClassification classification,
            OperatorClassification operatorClassification,
            TypeSignature declaringType,
            TypeSignature returnType,
            IEnumerable<string> genericParameters,
            IEnumerable<ParameterSignature> parameters)
        {
            _identity = identity;
            _name = name;
            _url = url;
            _classification = classification;
            _operatorClassification = operatorClassification;
            _returnType = returnType;
            _declaringType = declaringType;
            _genericParameters = new List<string>(genericParameters);
            _parameters = new List<ParameterSignature>(parameters);
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
        /// Gets the method URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the method classification.
        /// </summary>
        /// <value>The method classification.</value>
        public MethodClassification Classification
        {
            get { return _classification; }
        }

        /// <summary>
        /// Gets the operator classification.
        /// </summary>
        /// <value>The operator classification.</value>
        public OperatorClassification OperatorClassification
        {
            get { return _operatorClassification; }
        }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value>The return type.</value>
        public TypeSignature ReturnType
        {
            get { return _returnType; }
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
        /// Gets the method's generic parameters.
        /// </summary>
        /// <value>The method's generic parameters.</value>
        public List<string> GenericParameters
        {
            get { return _genericParameters; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IReadOnlyList<ParameterSignature> Parameters
        {
            get { return _parameters; }
        }
    }
}