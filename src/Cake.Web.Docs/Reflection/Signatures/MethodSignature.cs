using System.Collections.Generic;

namespace Cake.Web.Docs.Reflection.Signatures
{
    /// <summary>
    /// Represents a method signature.
    /// </summary>
    public sealed class MethodSignature
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
        /// Gets the method URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; }

        /// <summary>
        /// Gets the method classification.
        /// </summary>
        /// <value>The method classification.</value>
        public MethodClassification Classification { get; }

        /// <summary>
        /// Gets the operator classification.
        /// </summary>
        /// <value>The operator classification.</value>
        public OperatorClassification OperatorClassification { get; }

        /// <summary>
        /// Gets the return type.
        /// </summary>
        /// <value>The return type.</value>
        public TypeSignature ReturnType { get; }

        /// <summary>
        /// Gets the declaring type.
        /// </summary>
        /// <value>The declaring type.</value>
        public TypeSignature DeclaringType { get; }

        /// <summary>
        /// Gets the method's generic parameters.
        /// </summary>
        /// <value>The method's generic parameters.</value>
        public List<string> GenericParameters { get; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IReadOnlyList<ParameterSignature> Parameters { get; }

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
            Identity = identity;
            Name = name;
            Url = url;
            Classification = classification;
            OperatorClassification = operatorClassification;
            ReturnType = returnType;
            DeclaringType = declaringType;
            GenericParameters = new List<string>(genericParameters);
            Parameters = new List<ParameterSignature>(parameters);
        }
    }
}