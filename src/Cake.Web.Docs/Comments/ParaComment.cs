using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>para</code> comment.
    /// </summary>
    public sealed class ParaComment : AggregateComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParaComment"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        public ParaComment(IEnumerable<IComment> comments)
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
            visitor.VisitPara(this, context);
        }
    }
}
