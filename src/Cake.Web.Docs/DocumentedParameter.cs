using System.Diagnostics;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented parameter.
    /// </summary>
    [DebuggerDisplay("{Name,nq}")]
    public sealed class DocumentedParameter : DocumentedMember
    {
        private readonly ParameterDefinition _definition;
        private readonly ParamComment _comment;
        private readonly bool _isOutParameter;
        private readonly bool _isRefParameter;

        /// <summary>
        /// Gets the declaring method.
        /// </summary>
        /// <value>The method.</value>
        public DocumentedMethod Method { get; internal set; }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        /// <value>
        /// The parameter name.
        /// </value>
        public string Name
        {
            get { return _definition.Name; }
        }

        /// <summary>
        /// Gets the parameter comment.
        /// </summary>
        /// <value>The parameter comment.</value>
        public ParamComment Comment
        {
            get { return _comment; }
        }

        /// <summary>
        /// Gets the parameter definition.
        /// </summary>
        /// <value>The parameter definition.</value>
        public ParameterDefinition Definition
        {
            get { return _definition; }
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
        /// Initializes a new instance of the <see cref="DocumentedParameter"/> class.
        /// </summary>
        /// <param name="definition">The parameter definition.</param>
        /// <param name="comment">The parameter comment.</param>
        public DocumentedParameter(ParameterDefinition definition, ParamComment comment)
            : base(MemberClassification.Parameter,  null, null, null)
        {
            _definition = definition;
            _comment = comment;
            _isOutParameter = definition.IsOut;
            _isRefParameter = definition.ParameterType is ByReferenceType;
        }
    }
}
