using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cake.Web.Docs.Comments;

namespace Cake.Web.Docs.Xml.Model
{
    /// <summary>
    /// Represents an XML documentation member.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay(),nq}")]
    public sealed class XmlDocumentationMember
    {
        private readonly string _cref;
        private readonly List<IComment> _comments;

        /// <summary>
        /// Gets the Identity.
        /// </summary>
        /// <value>The Identity.</value>
        public string CRef
        {
            get { return _cref; }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public IReadOnlyList<IComment> Comments
        {
            get { return _comments; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDocumentationMember"/> class.
        /// </summary>
        /// <param name="cref">The cref identity.</param>
        /// <param name="comments">The comments.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Identity
        /// or
        /// comments
        /// </exception>
        /// <exception cref="System.InvalidOperationException">Identity cannot be empty.</exception>
        public XmlDocumentationMember(string cref, IEnumerable<IComment> comments)
        {
            if (cref == null)
            {
                throw new ArgumentNullException("cref");
            }
            if (string.IsNullOrWhiteSpace(cref))
            {
                throw new InvalidOperationException("Identity cannot be empty.");
            }
            if (comments == null)
            {
                throw new ArgumentNullException("comments");
            }
            _cref = cref;
            _comments = new List<IComment>(comments);
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private string DebuggerDisplay()
        {
            return _cref;
        }
    }
}
