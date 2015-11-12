using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents an aggregate comment.
    /// </summary>
    public abstract class AggregateComment : Comment
    {
        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        public IReadOnlyList<IComment> Children { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateComment"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        protected AggregateComment(IEnumerable<IComment> comments)
        {
            Children = new List<IComment>(comments);
        }
    }
}
