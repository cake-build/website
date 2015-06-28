using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents an aggregate comment.
    /// </summary>
    public abstract class AggregateComment : Comment
    {
        private readonly List<IComment> _children;

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        public IReadOnlyList<IComment> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateComment"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        protected AggregateComment(IEnumerable<IComment> comments)
        {
            _children = new List<IComment>(comments);
        }
    }
}
