using System.Diagnostics;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;
using Cake.Web.Docs.Reflection.Model;
using Mono.Cecil;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented fields.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedField : DocumentedMember
    {
        private readonly FieldDefinition _definition;
        private readonly string _identity;

        /// <summary>
        /// Gets the field's identity.
        /// </summary>
        /// <value>The field's identity.</value>
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
        /// Gets the field definition.
        /// </summary>
        /// <value>The field definition.</value>
        public FieldDefinition Definition
        {
            get { return _definition; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedField"/> class.
        /// </summary>
        /// <param name="info">The field info.</param>
        /// <param name="summary">The summary comment.</param>
        /// <param name="remarks">The remarks comment.</param>
        /// <param name="example">The example comment.</param>
        public DocumentedField(
            IFieldInfo info,
            SummaryComment summary, 
            RemarksComment remarks,
            ExampleComment example)
            : base(MemberClassification.Type, summary, remarks, example)
        {
            _definition = info.Definition;
            _identity = info.Identity;
        }
    }
}
