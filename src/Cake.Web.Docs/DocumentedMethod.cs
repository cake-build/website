using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented method.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedMethod : DocumentedMember
    {
        /// <summary>
        /// Gets the method's identity.
        /// </summary>
        /// <value>The method's identity.</value>
        public string Identity { get; }

        /// <summary>
        /// Gets the declaring type.
        /// </summary>
        /// <value>The declaring type.</value>
        public DocumentedType Type { get; internal set; }

        /// <summary>
        /// Gets the return value comment.
        /// </summary>
        /// <value>
        /// The return value comment.
        /// </value>
        public ReturnsComment Returns { get; }

        /// <summary>
        /// Gets the method's parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IReadOnlyList<DocumentedParameter> Parameters { get; }

        /// <summary>
        /// Gets the method definition.
        /// </summary>
        /// <value>The method definition.</value>
        public MethodDefinition Definition { get; }

        /// <summary>
        /// Gets the method classification.
        /// </summary>
        /// <value>The method classification.</value>
        public MethodClassification MethodClassification { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedMethod" /> class.
        /// </summary>
        /// <param name="info">The method info.</param>
        /// <param name="parameters">The method's parameters.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="examples">The example comments.</param>
        /// <param name="returns">The return value comment.</param>
        /// <param name="metadata">The method metadata.</param>
        public DocumentedMethod(
            IMethodInfo info,
            IEnumerable<DocumentedParameter> parameters,
            SummaryComment summary,
            RemarksComment remarks,
            IEnumerable<ExampleComment> examples,
            ReturnsComment returns,
            IDocumentationMetadata metadata)
            : base(MemberClassification.Method, summary, remarks, examples, metadata)
        {
            Definition = info.Definition;
            MethodClassification = MethodClassifier.GetMethodClassification(info.Definition);
            Identity = info.Identity;
            Parameters = new List<DocumentedParameter>(parameters);
            Returns = returns;
        }
    }
}