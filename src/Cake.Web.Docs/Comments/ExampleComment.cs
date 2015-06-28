using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents an <code>example</code> comment.
    /// </summary>
    public sealed class ExampleComment : AggregateComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleComment"/> class.
        /// </summary>
        /// <param name="comments">The comments.</param>
        public ExampleComment(IEnumerable<IComment> comments) 
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
            visitor.VisitExample(this, context);
        }
    }
}
