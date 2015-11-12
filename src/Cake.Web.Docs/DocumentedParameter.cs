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
        public string Name => Definition.Name;

        /// <summary>
        /// Gets the parameter comment.
        /// </summary>
        /// <value>The parameter comment.</value>
        public ParamComment Comment { get; }

        /// <summary>
        /// Gets the parameter definition.
        /// </summary>
        /// <value>The parameter definition.</value>
        public ParameterDefinition Definition { get; }

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
        /// Initializes a new instance of the <see cref="DocumentedParameter"/> class.
        /// </summary>
        /// <param name="definition">The parameter definition.</param>
        /// <param name="comment">The parameter comment.</param>
        /// <param name="metadata">The associated metadata.</param>
        public DocumentedParameter(ParameterDefinition definition, ParamComment comment, IDocumentationMetadata metadata)
            : base(MemberClassification.Parameter,  null, null, null, metadata)
        {
            Definition = definition;
            Comment = comment;
            IsOutParameter = definition.IsOut;
            IsRefParameter = definition.ParameterType is ByReferenceType;
        }
    }
}
