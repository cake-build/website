using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs.Comments;
using Cake.Web.Docs.Reflection;

namespace Cake.Web.Docs
{
    /// <summary>
    /// Represents a documented namespace.
    /// </summary>
    [DebuggerDisplay("{Identity,nq}")]
    public sealed class DocumentedNamespace : DocumentedMember
    {
        private readonly string _identity;
        private readonly string _name;
        private readonly List<DocumentedType> _types;

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>The identity.</value>
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
        /// Gets the assembly this namespace is located in.
        /// </summary>
        /// <value>The assembly this namespace is located in.</value>
        public DocumentedAssembly Assembly { get; internal set; }

        /// <summary>
        /// Gets the namespace tree.
        /// </summary>
        /// <value>The namespace tree.</value>
        public DocumentedNamespaceTree Tree { get; internal set; }

        /// <summary>
        /// Gets the types in this namespace.
        /// </summary>
        /// <value>The types in this namespace.</value>
        public IReadOnlyList<DocumentedType> Types
        {
            get { return _types; }
        }

        /// <summary>
        /// Gets a value indicating whether this namespace has content.
        /// </summary>
        /// <value>
        /// <c>true</c> if this namespace has content; otherwise, <c>false</c>.
        /// </value>
        public bool HasContent
        {
            get { return _types.Count > 0; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentedNamespace"/> class.
        /// </summary>
        /// <param name="identity">The Identity.</param>
        /// <param name="name">The namespace name.</param>
        /// <param name="types">The types.</param>
        /// <param name="summaryComment">The summary comment.</param>
        /// <param name="metadata">The associated metadata.</param>
        public DocumentedNamespace(
            string identity, 
            string name,
            IEnumerable<DocumentedType> types,
            SummaryComment summaryComment,
            IDocumentationMetadata metadata)
            : base(MemberClassification.Namespace, summaryComment, null, null, metadata)
        {
            _identity = identity;
            _name = name;
            _types = new List<DocumentedType>(types);
        }
    }
}