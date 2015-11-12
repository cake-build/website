using System.Collections.Generic;

namespace Cake.Web.Docs.Comments
{
    /// <summary>
    /// Represents a <code>param</code> comment.
    /// </summary>
    public sealed class ParamComment : AggregateComment
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamComment"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="comments">The comments.</param>
        public ParamComment(string name, IEnumerable<IComment> comments)
            : base(comments)
        {
            Name = name;
        }

        /// <summary>
        /// Accepts the specified visitor.
        /// </summary>
        /// <typeparam name="T">The context type.</typeparam>
        /// <param name="visitor">The visitor.</param>
        /// <param name="context">The context.</param>
        public override void Accept<T>(CommentVisitor<T> visitor, T context)
        {
            visitor.VisitParam(this, context);
        }
    }
}
