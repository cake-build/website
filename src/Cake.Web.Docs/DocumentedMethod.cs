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
        private readonly MethodDefinition _definition;
        private readonly MethodClassification _methodClassification;
        private readonly string _identity;        
        private readonly List<DocumentedParameter> _parameters;
        private readonly ReturnsComment _returns;

        /// <summary>
        /// Gets the method's identity.
        /// </summary>
        /// <value>The method's identity.</value>
        public string Identity
        {
            get { return _identity; }
        }

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
        public ReturnsComment Returns
        {
            get { return _returns; }
        }

        /// <summary>
        /// Gets the method's parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IReadOnlyList<DocumentedParameter> Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Gets the method definition.
        /// </summary>
        /// <value>The method definition.</value>
        public MethodDefinition Definition
        {
            get { return _definition; }
        }

        /// <summary>
        /// Gets the method classification.
        /// </summary>
        /// <value>The method classification.</value>
        public MethodClassification MethodClassification
        {
            get { return _methodClassification; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedMethod" /> class.
        /// </summary>
        /// <param name="info">The method info.</param>
        /// <param name="parameters">The method's parameters.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="examples">The example comments.</param>
        /// <param name="returns">The return value comment.</param>
        public DocumentedMethod(
            IMethodInfo info, 
            IEnumerable<DocumentedParameter> parameters,
            SummaryComment summary, 
            RemarksComment remarks, 
            IEnumerable<ExampleComment> examples,
            ReturnsComment returns) : base(MemberClassification.Method, summary, remarks, examples)
        {
            _definition = info.Definition;
            _methodClassification = MethodClassifier.GetMethodClassification(info.Definition);
            _identity = info.Identity;
            _parameters = new List<DocumentedParameter>(parameters);
            _returns = returns;
        }        
    }
}