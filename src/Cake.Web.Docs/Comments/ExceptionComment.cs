using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents an <code>exception</code> comment.
    /// </summary>
    public sealed class ExceptionComment : AggregateComment
    {
        private readonly string _member;

        /// <summary>
        /// Gets the member.
        /// </summary>
        /// <value>The member.</value>
        public string Member
        {
            get { return _member; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionComment"/> class.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="comments">The comments.</param>
        public ExceptionComment(string member, IEnumerable<IComment> comments) 
            : base(comments)
        {
            _member = member;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitException(this, context);
        }
    }
}
