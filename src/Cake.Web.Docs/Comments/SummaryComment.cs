using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>summary</code> comment.
    /// </summary>
    public sealed class SummaryComment : AggregateComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SummaryComment"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        public SummaryComment(IEnumerable<IComment> comments) 
            : base(comments)
        {
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitSummary(this, context);
        }
    }
}
